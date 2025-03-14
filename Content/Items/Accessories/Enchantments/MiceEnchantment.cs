using ClickerClass;
using ClickerClass.Items.Armors;
using ClickerClass.Items.Weapons.Clickers;
using FargoClickers.Common;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.UI.Elements;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Accessories.Enchantments
{
    public class MiceEnchantment : BaseEnchant
    {
        public override Color nameColor => new Color(177, 179, 224);
        public static Texture2D buffTexture;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            if (!Main.dedServ)
            {
                buffTexture = ModContent.Request<Texture2D>("FargoClickers/Content/Items/Accessories/Enchantments/MiceEnchantmentBuff").Value;
            }
            ClickerSystem.RegisterClickerItem(this);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Red;
            Item.value = 10000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MiceEffect>(Item);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MiceMask>()
                .AddIngredient<MiceSuit>()
                .AddIngredient<MiceBoots>()

                .AddIngredient<MiceClicker>()
                .AddIngredient<AstralClicker>()
                .AddIngredient<LordsClicker>()

                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
    public class MiceEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        //public override int ToggleItemType => base .ToggleItemType;
        public override int ToggleItemType => ModContent.ItemType<MiceEnchantment>();
        public override void PostUpdateEquips(Player player)
        {
            FargoClickerPlayer modPlayer = player.FargoClickerPlayer();

            if (player.HasEffect<MatrixForceEffect>())
                return;

            modPlayer.MiceEnch = true;
            if (modPlayer.miceHurtTimer > 0)
                modPlayer.miceHurtTimer--;

            if (modPlayer.miceCooldownTimer > 0 && modPlayer.miceCooldownTimerMax > 0)
            {
                modPlayer.miceCooldownTimer--;
                CooldownBarManager.Activate("MiceCooldown", ModContent.Request<Texture2D>("FargoClickers/Content/Items/Accessories/Enchantments/MiceEnchantment").Value, new Color(98, 101, 145),
                    () => Main.LocalPlayer.FargoClickerPlayer().miceCooldownTimerRatio, true, activeFunction: () => player.HasEffect<MiceEffect>());
            }
            if (modPlayer.miceCooldownTimer == 0)
            {
                //modPlayer.miceHurtTriggered = false;
                modPlayer.miceCooldownTimerMax = 0;
            }

            if (modPlayer.miceBuffTimer > 0)
            {
                modPlayer.miceBuffTimer--;
                player.moveSpeed += 0.1f;
                player.runAcceleration += 0.1f;
                CooldownBarManager.Activate("MiceBuff", MiceEnchantment.buffTexture, new Color(177, 179, 224),
                    () => (float)Main.LocalPlayer.FargoClickerPlayer().miceBuffTimer / Main.LocalPlayer.FargoClickerPlayer().miceBuffTimerMax, true, activeFunction: () => player.HasEffect<MiceEffect>());
            }
        }
        public override void OnHurt(Player player, Player.HurtInfo info)
        {
            FargoClickerPlayer modPlayer = Main.LocalPlayer.FargoClickerPlayer();

            if (player.HasEffect<MatrixForceEffect>())
                return;

            if (modPlayer.miceHurtTimer > 0 /*&& !modPlayer.miceHurtTriggered*/)
            {
                player.immuneTime = player.ForceEffect<MiceEffect>() ? 180 : 120;
                modPlayer.miceBuffTimer = modPlayer.miceBuffTimerMax;
                modPlayer.miceCooldownTimer += 59 * 60;
                modPlayer.miceCooldownTimerMax = 3600;
                modPlayer.miceHurtTimer = 0;
            }
        }
        public override float ModifyUseSpeed(Player player, Item item)
        {
            if (player.FargoClickerPlayer().miceBuffTimer > 0)
                return 0.1f;
            return 0;
        }
    }
}
