using FargoClickers.Content.Items.Accessories;
using FargoClickers.Content.Items.Accessories.Enchantments;
using FargowiltasSouls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargoClickers
{
    public class FargoClickerPlayer : ModPlayer
    {
        //public float AttackSpeed = 1f;
        public bool HaveCheckedAttackSpeed = false;

        //Motherboard
        public bool MotherboardEnch = false;
        public float motherboardPower = 1f;
        public float motherboardPowerMax = 2f;
        public float motherboardPowerStep = 0.01f;
        public int motherboardTime = 0;

        //Precursor
        public bool PrecursorEnch = false;

        //Overclock
        public bool OverclockEnch = false;
        public int overclockCounter = 0;
        public int overclockBuffTime = 0;
        public int overclockBuffTimeMax => Player.HasEffect<MatrixForceEffect>() ? 900 : (Player.ForceEffect<OverclockEffect>() ? 600 : 300);
        public bool overclockActive = false;
        public int overclockCooldownTime = 0;
        public int overclockCooldownTimeMax => 3600;

        //Mice
        public bool MiceEnch = false;
        public int miceHurtTimer;
        public int miceHurtTimerMax => 60;
        //public bool miceHurtTriggered = false;
        public int miceBuffTimer = 0;
        public int miceBuffTimerMax => Player.ForceEffect<MiceEffect>() ? 900 : 600;
        public int miceCooldownTimer = 0;
        public int miceCooldownTimerMax = 0;
        public float miceCooldownTimerRatio
        {
            get
            {
                if (miceCooldownTimer == 0 && miceCooldownTimerMax == 0)
                    return 0f;
                float num = (float)miceCooldownTimer / miceCooldownTimerMax;
                if (num > 1)
                    return 1f;
                if (num < 0)
                    return 0f;
                return num;
            }
        }

        //Force of Matrix
        public int matrixBuffTimer = 0;
        public int matrixBuffTimerMax => 900;


        public int MiceBoosterTimer = 0;
        public override void ResetEffects()
        {
            //AttackSpeed = 1f;
            HaveCheckedAttackSpeed = false;

            MotherboardEnch = false;
            PrecursorEnch = false;
            OverclockEnch = false;
            MiceEnch = false;

            motherboardPowerMax = 2f;
            motherboardPowerStep = 0.01f;
        }
        public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (MotherboardEnch && ClickerCompat.IsClickerWeapon(item))
            {
                if (Player.Clicker().clickerPerSecond > 6)
                {
                    motherboardPower += motherboardPowerStep * (Player.Clicker().clickerPerSecond - 6);
                    if (motherboardPower > motherboardPowerMax)
                        motherboardPower = motherboardPowerMax;
                    motherboardTime = 30;
                }

                damage = (int)(damage * motherboardPower);
            }
        }
        public override void PostUpdateMiscEffects()
        {
            if (Player.FargoSouls().Berserked)
            {
                ClickerCompat.SetAutoReuseEffect(Player, 10f, false, true);
            }
        }
        public override void PostUpdateEquips()
        {
            if (MiceBoosterTimer > 0)
            {
                MiceBoosterTimer--;
                Player.statDefense += 15;
            }
        }
    }
}
