using ClickerClass;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Accessories
{
    public class MasterPlayerSoul : BaseSoul
    {
        public static readonly Color ItemColor = new(83, 162, 255);
        protected override Color? nameColor => ItemColor;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //Basics
            player.GetDamage<ClickerDamage>() += 0.22f;
            player.GetCritChance<ClickerDamage>() += 10;
            player.Clicker().clickerRadius += 1.5f;
            player.Clicker().clickerBonusPercent += 0.2f;

            //Gamer Crate 
            ClickerCompat.SetAutoReuseEffect(player, 7f, true);
            if (!hideVisual)
            {
                player.Clicker().accEnchantedLED = true;
                player.Clicker().accEnchantedLED2 = true;
            }

            //Chocolate Milk and Cookies
            player.Clicker().EnableClickEffect(ClickEffect.ChocolateChip);
            player.Clicker().accCookieItem = Item;
            player.Clicker().accCookie2 = true;
            player.Clicker().accGlassOfMilk = true;

            //AIM Module
            player.Clicker().accAimbotModule = true;
            player.Clicker().accAimbotModule2 = true;

            //SMedal
            player.Clicker().accSMedalItem = Item;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GamerEssence>()
                .AddIngredient<GamerCrate>()
                .AddIngredient<ChocolateMilkCookies>()
                .AddIngredient<AimbotModule>()
                .AddIngredient<SMedal>()
                .AddIngredient<SpiralClicker>()
                .AddIngredient<ChlorophyteClicker>()
                .AddIngredient<MouseClicker>()
                .AddIngredient<EclipticClicker>()
                .AddIngredient<LanternClicker>()
                .AddIngredient<FrozenClicker>()
                .AddIngredient<HighTechClicker>()
                .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"))
                .Register();
        }

    }
}
