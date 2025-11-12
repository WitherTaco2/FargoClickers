using ClickerClass;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace FargoClickers.Content.Items.Accessories
{
    public class GamerEssence : BaseEssence
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            ClickerSystem.RegisterClickerItem(this);
        }
        public override Color nameColor => new(83, 162, 255);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<ClickerDamage>() += 0.18f;
            player.GetCritChance<ClickerDamage>() += 8;
            player.GetModPlayer<ClickerPlayer>().clickerRadius += 0.2f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<WoodenClicker>()
                .AddIngredient<BalloonClicker>()
                .AddIngredient<StarryClicker>()
                .AddIngredient<TorchClicker>()
                .AddIngredient<HoneyGlazedClicker>()
                .AddIngredient<ImpishClicker>()
                .AddIngredient<UmbralClicker>()

                .AddIngredient<ClickerEmblem>()
                .AddIngredient(ItemID.HallowedBar, 5)

                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
