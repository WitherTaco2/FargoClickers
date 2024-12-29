using ClickerClass.Items.Armors;
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
    public class MiceEnchantment : BaseEnchant
    {
        public override Color nameColor => new Color(177, 179, 224);
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
        public override int ToggleItemType => ModContent.ItemType<MiceEnchantment>();

    }
}
