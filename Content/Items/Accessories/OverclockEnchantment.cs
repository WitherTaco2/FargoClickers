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
    public class OverclockEnchantment : BaseEnchant
    {
        public override Color nameColor => new Color(214, 84, 91);
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Yellow;
            Item.value = 10000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OverclockEffect>(Item);
            player.AddEffect<RegalClickingGloveEffect>(Item);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<OverclockHelmet>()
                .AddIngredient<OverclockSuit>()
                .AddIngredient<OverclockBoots>()

                .AddIngredient<RegalClickingGlove>()
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

    }
    public class RegalClickingGloveEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<OverclockEnchantment>();
        public override void PostUpdateEquips(Player player)
        {
            player.Clicker().accRegalClickingGlove = true;
        }
    }
}
