using ClickerClass.Dusts;
using ClickerClass.Utilities;
using FargoClickers.Content.Items.Accessories;
using FargoClickers.Content.Items.Accessories.Enchantments;
using FargowiltasSouls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers
{
    public class FargoClickerPlayer : ModPlayer
    {
        //public float AttackSpeed = 1f;
        public bool HaveCheckedAttackSpeed = false;

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
        /*public bool matrixActive = false;
        public int matrixCooldownTimer = 0;
        public int matrixCooldownTimerMax = 0;
        public float matrixCooldownTimerRatio
        {
            get
            {
                if (matrixCooldownTimer == 0 && matrixCooldownTimerMax == 0)
                    return 0f;
                float num = (float)matrixCooldownTimer / matrixCooldownTimerMax;
                if (num > 1)
                    return 1f;
                if (num < 0)
                    return 0f;
                return num;
            }
        }*/

        //public Vector2[] precursorPos = new Vector2[10] { new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0) };

        public override void ResetEffects()
        {
            //AttackSpeed = 1f;
            HaveCheckedAttackSpeed = false;

            PrecursorEnch = false;
            OverclockEnch = false;
            MiceEnch = false;
        }
        /*public override float UseSpeedMultiplier(Item item)
        {
            int useTime = item.useTime;
            int useAnimate = item.useAnimation;
            if (useTime <= 0 || useAnimate <= 0 || item.damage <= 0)
                return base.UseSpeedMultiplier(item);

            if (!HaveCheckedAttackSpeed)
            {
                HaveCheckedAttackSpeed = true;

                if (Player.HasEffect<OverclockEffect>() && overclockBuffTime > 0)
                {
                    if (item.DamageType != DamageClass.Default && item.pick == 0 && item.axe == 0 && item.hammer == 0 && item.type != ModContent.ItemType<PrismaRegalia>())
                    {
                        AttackSpeed *= Player.ForceEffect<OverclockEffect>() ? 2f : 1.5f;
                    }
                }
                if (Player.HasEffect<MiceEffect>() && miceBuffTimer > 0)
                {
                    if (item.DamageType != DamageClass.Default && item.pick == 0 && item.axe == 0 && item.hammer == 0 && item.type != ModContent.ItemType<PrismaRegalia>())
                    {
                        AttackSpeed -= .1f;
                    }
                }

                //checks so weapons dont break
                while (useTime / AttackSpeed < 1)
                {
                    AttackSpeed -= .01f;
                }

                while (useAnimate / AttackSpeed < 3)
                {
                    AttackSpeed -= .01f;
                }

                if (AttackSpeed < .1f)
                    AttackSpeed = .1f;
            }



            return AttackSpeed;
        }*/
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (FargoClickers.MiceTeleport.JustPressed && MiceEnch && miceCooldownTimer == 0 && Main.myPlayer == Player.whoAmI && !Player.CCed)
            {
                Vector2 vector = default(Vector2);
                vector.X = Main.mouseX + Main.screenPosition.X;
                if (Player.gravDir == 1f)
                {
                    vector.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)base.Player.height;
                }
                else
                {
                    vector.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                }

                vector.X -= base.Player.width / 2;
                if (vector.X > 50f && vector.X < (float)(Main.maxTilesX * 16 - 50) && vector.Y > 50f && vector.Y < (float)(Main.maxTilesY * 16 - 50) && !Collision.SolidCollision(vector, base.Player.width, base.Player.height))
                {
                    //Player.Teleport(vector, 4);
                    //NetMessage.SendData(65, -1, -1, null, 0, base.Player.whoAmI, vector.X, vector.Y, 1);

                    //Teleport itself
                    Vector2 teleportPos = Main.MouseWorld;
                    SoundEngine.PlaySound(SoundID.Item115, teleportPos);

                    Player.ClickerTeleport(teleportPos);
                    NetMessage.SendData(MessageID.PlayerControls, number: Player.whoAmI);


                    //Buff
                    miceHurtTimer = miceHurtTimerMax;
                    miceCooldownTimer = 600;
                    miceCooldownTimerMax = 600;


                    //Dust
                    float num102 = 50f;
                    int num103 = 0;
                    while ((float)num103 < num102)
                    {
                        Vector2 vector12 = Vector2.UnitX * 0f;
                        vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (MathHelper.TwoPi / num102)), default(Vector2)) * new Vector2(2f, 2f);
                        vector12 = vector12.RotatedBy((double)Vector2.Zero.ToRotation(), default(Vector2));
                        int num104 = Dust.NewDust(teleportPos, 0, 0, ModContent.DustType<MiceDust>(), 0f, 0f, 0, default(Color), 2f);
                        Main.dust[num104].noGravity = true;
                        Main.dust[num104].position = teleportPos + vector12;
                        Main.dust[num104].velocity = Vector2.Zero * 0f + vector12.SafeNormalize(Vector2.UnitY) * 4f;
                        int num = num103;
                        num103 = num + 1;
                    }
                }
            }
        }
    }
}
