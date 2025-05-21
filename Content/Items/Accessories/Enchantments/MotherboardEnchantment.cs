using ClickerClass;
using ClickerClass.Items.Armors;
using ClickerClass.Items.Misc;
using ClickerClass.Items.Weapons.Clickers;
using FargoClickers.Common;
using FargowiltasSouls;
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

                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
    public class MotherboardEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<MotherboardEnchantment>();

        public override void PostUpdateEquips(Player player)
        {
            FargoClickerPlayer modPlayer = player.FargoClickerPlayer();

            modPlayer.MotherboardEnch = true;

            if (modPlayer.motherboardTime > 0)
                modPlayer.motherboardTime--;
            else
            {
                modPlayer.motherboardPower -= modPlayer.motherboardPowerStep;
                if (modPlayer.motherboardPower < 1f)
                    modPlayer.motherboardPower = 1f;
                modPlayer.motherboardTime = 3;
            }


            if (player.HasEffect<MatrixForceEffect>())
            {
                modPlayer.motherboardPowerMax += 1f;
                modPlayer.motherboardPowerStep += 0.02f;
            }
            else if (player.ForceEffect<MotherboardEffect>())
            {
                modPlayer.motherboardPowerStep += 0.01f;
            }
        }
    }
}
