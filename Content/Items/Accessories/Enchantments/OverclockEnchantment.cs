using ClickerClass;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Armors;
using ClickerClass.Items.Placeable;
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
    public class OverclockEnchantment : BaseEnchant
    {
        public override Color nameColor => new Color(214, 84, 91);
        public static Texture2D buffTexture;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            if (!Main.dedServ)
            {
                buffTexture = ModContent.Request<Texture2D>("FargoClickers/Content/Items/Accessories/Enchantments/OverclockEnchantmentBuff").Value;
            }
            ClickerSystem.RegisterClickerItem(this);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Yellow;
            Item.value = 10000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OverclockEffect>(Item);
            player.AddEffect<OverclockBottomlessBoxofPaperclipsEffect>(Item);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<OverclockHelmet>()
                .AddIngredient<OverclockSuit>()
                .AddIngredient<OverclockBoots>()

                .AddIngredient<BottomlessBoxofPaperclips>()
                .AddIngredient<ArthursClicker>()
                .AddIngredient<ABlissfulDay>()

                .AddTile(TileID.CrystalBall)
                .Register();
        }
    }
    public class OverclockEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<OverclockEnchantment>();
        public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
        {
            if (!player.FargoClickerPlayer().overclockActive && !player.HasEffect<MatrixForceEffect>())
                player.FargoClickerPlayer().overclockCounter += hitInfo.Damage;
        }
        public override void PostUpdateEquips(Player player)
        {
            FargoClickerPlayer modPlayer = Main.LocalPlayer.FargoClickerPlayer();

            if (player.HasEffect<MatrixForceEffect>())
                return;

            modPlayer.OverclockEnch = true;

            //Texture2D GetTexture(Texture2D t) => player.HasEffect<MatrixForceEffect>() ? ForceOfMatrix.forceTexture : t;

            if (modPlayer.overclockBuffTime > 0)
            {
                CooldownBarManager.Activate("Overclock", OverclockEnchantment.buffTexture, new Color(214, 84, 91),
                    () => (float)Main.LocalPlayer.FargoClickerPlayer().overclockBuffTime / Main.LocalPlayer.FargoClickerPlayer().overclockBuffTimeMax, true, activeFunction: () => player.HasEffect<OverclockEffect>());

                modPlayer.overclockBuffTime--;
            }
            if (modPlayer.overclockCooldownTime > 0)
            {
                modPlayer.overclockCooldownTime--;
                CooldownBarManager.Activate("OverclockCooldown", ModContent.Request<Texture2D>("FargoClickers/Content/Items/Accessories/Enchantments/OverclockEnchantment").Value, new Color(106, 26, 30),
                    () => (float)Main.LocalPlayer.FargoClickerPlayer().overclockCooldownTime / Main.LocalPlayer.FargoClickerPlayer().overclockCooldownTimeMax, true, activeFunction: () => player.HasEffect<OverclockEffect>());
            }

            if (modPlayer.overclockCounter > 10000 && modPlayer.overclockCooldownTime == 0)
            {
                if (!modPlayer.overclockActive)
                    modPlayer.overclockBuffTime = modPlayer.overclockBuffTimeMax;
                modPlayer.overclockActive = true;
            }
            if (modPlayer.overclockBuffTime == 0 && modPlayer.overclockActive)
            {
                modPlayer.overclockCounter = 0;
                modPlayer.overclockCooldownTime = modPlayer.overclockCooldownTimeMax;
                modPlayer.overclockActive = false;
            }
        }
        public override float ModifyUseSpeed(Player player, Item item)
        {
            if (player.FargoClickerPlayer().overclockBuffTime > 0 && !player.HasEffect<MatrixForceEffect>())
                return player.ForceEffect<OverclockEffect>() ? player.FargoSouls().AttackSpeed : (player.FargoSouls().AttackSpeed / 2);
            return 0;
        }
    }
    public class OverclockBottomlessBoxofPaperclipsEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<OverclockEnchantment>();
        public override void PostUpdateEquips(Player player)
        {
            player.Clicker().accPaperclipsItem = player.EffectItem<OverclockBottomlessBoxofPaperclipsEffect>();
        }
    }
}
