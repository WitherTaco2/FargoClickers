using ClickerClass;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Armors;
using ClickerClass.Items.Placeable;
using ClickerClass.Items.Weapons.Clickers;
using FargoClickers.Common;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Accessories
{
    public class RGBEnchantment : BaseEnchant
    {
        public override Color nameColor => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 50);
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Orange;
            Item.value = 10000;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color?(nameColor);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<RGBEffect>(Item);
            player.AddEffect<BigRedButtonEffect>(Item);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<RGBHelm>()
                .AddIngredient<RGBBreastplate>()
                .AddIngredient<RGBGreaves>()

                .AddIngredient<BigRedButton>()
                .AddIngredient<CrystalClicker>()
                .AddIngredient<OutsideTheCave>()

                .AddTile(TileID.CrystalBall)
                .Register();
        }
    }
    public class RGBEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<RGBEnchantment>();

    }
    public class BigRedButtonEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<RGBEnchantment>();
        public override void PostUpdateEquips(Player player)
        {
            player.Clicker().EnableClickEffect(ClickEffect.BigRedButton);
        }
    }
}
