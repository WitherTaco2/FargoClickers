using ClickerClass;
using ClickerClass.Items.Armors;
using ClickerClass.Items.Misc;
using ClickerClass.Items.Weapons.Clickers;
using FargoClickers.Common;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Accessories.Enchantments
{
    public class MotherboardEnchantment : BaseEnchant
    {
        public override Color nameColor => new Color(148, 183, 224);
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ClickerSystem.RegisterClickerItem(this);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Orange;
            Item.value = 10000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MotherboardEffect>(Item);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MotherboardHelmet>()
                .AddIngredient<MotherboardSuit>()
                .AddIngredient<MotherboardBoots>()

                .AddIngredient<SFXButtonA>()
                .AddIngredient<SpaceClicker>()
                .AddIngredient<RedHotClicker>()

                .AddTile(TileID.CrystalBall)
                .Register();
        }
    }
    public class MotherboardEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<MotherboardEnchantment>();

    }
}
