using ClickerClass;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
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

            if (ModLoader.TryGetMod("CalamityClickers", out var calClicker) && ModLoader.HasMod("FargowiltasCrossmod"))
            {
                ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

                //D.o.G.
                clickerPlayer.clickerRadius += 1f;
                player.GetDamage<ClickerDamage>() += 0.3f;
                clickerPlayer.clickerBonusPercent -= 0.2f;
                ClickerCompat.SetAutoReuseEffect(player, 6f, true);
                clickerPlayer.accAimbotModule = true;
                clickerPlayer.accAimbotModule2 = true;

                //Bloody Choc n' Cookies
                calClicker.Call("SetAccessoryItem", "BloodyChocCookies", Item);
                clickerPlayer.accGlassOfMilk = true;

                //SS Medal
                calClicker.Call("SetAccessoryItem", "SSMedal", Item);

                //Cosmic Clicking Glove
                player.Clicker().accRegalClickingGlove = true;
                player.Clicker().accTriggerFinger = true;
                calClicker.Call("SetAccessoryItem", "FingerOfBloodGod", Item);
                calClicker.Call("SetAccessoryItem", "LihzahrdParticleAccelerator", Item, 100);

            }
            else
            {
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
        }
        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("CalamityClickers", out var calClicker) && ModLoader.HasMod("FargowiltasCrossmod"))
            {
                CreateRecipe()
                    .AddIngredient<GamerEssence>()
                    .AddIngredient(calClicker.Find<ModItem>("DoG").Type)
                    .AddIngredient(calClicker.Find<ModItem>("BloodyChocCookies").Type)
                    .AddIngredient(calClicker.Find<ModItem>("SSMedal").Type)
                    .AddIngredient(calClicker.Find<ModItem>("CosmicClickingGlove").Type)

                    .AddIngredient(calClicker.Find<ModItem>("InfestedBeeClicker").Type)
                    .AddIngredient(calClicker.Find<ModItem>("HolyGoldenClicker").Type)
                    .AddIngredient(calClicker.Find<ModItem>("PolterplasmClicker").Type)
                    .AddIngredient(calClicker.Find<ModItem>("NightmareClicker").Type)
                    .AddIngredient(calClicker.Find<ModItem>("ClickerNova").Type)

                    .AddIngredient<AbomEnergy>(10)

                    .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"))
                    .Register();

            }
            else
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
