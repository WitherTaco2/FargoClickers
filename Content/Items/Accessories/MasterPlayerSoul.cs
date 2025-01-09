using ClickerClass;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Accessories
{
    public class MasterPlayerSoul : BaseSoul
    {
        public static readonly Color ItemColor = new(83, 162, 255);
        protected override Color? nameColor => ItemColor;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((ModLoader.TryGetMod("CalamityClickers", out var calClicker) && ModLoader.HasMod("FargowiltasCrossmod")) ? ILocalizedModTypeExtensions.GetLocalizedValue(this, "CalamityAccessories") : ILocalizedModTypeExtensions.GetLocalizedValue(this, "NormalAccessories"));
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            ClickerSystem.RegisterClickerItem(this);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //Basics
            player.GetDamage<ClickerDamage>() += 0.22f;
            player.GetCritChance<ClickerDamage>() += 10;
            player.Clicker().clickerRadius += 1.5f;
            player.Clicker().clickerBonusPercent -= 0.2f;

            UpdateMasterPlayerSoulAccessories(Item, player, hideVisual);
        }
        public static void UpdateMasterPlayerSoulAccessories(Item Item, Player player, bool hideVisual)
        {
            if (ModLoader.TryGetMod("CalamityClickers", out var calClicker) && ModLoader.HasMod("FargowiltasCrossmod"))
            {
                ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();

                //D.o.G.
                ClickerCompat.SetAutoReuseEffect(player, 6f, true);
                if (!hideVisual)
                {
                    player.Clicker().accEnchantedLED = true;
                    player.Clicker().accEnchantedLED2 = true;
                }
                clickerPlayer.accAimbotModule = true;
                clickerPlayer.accAimbotModule2 = true;

                //Bloody Choc n' Cookies
                if (player.AddEffect<ChocolateChipEffect>(Item))
                    calClicker.Call("SetAccessoryItem", player, "BloodyChocCookies", Item);
                clickerPlayer.accGlassOfMilk = true;

                //Cosmic Clicking Glove
                player.Clicker().accRegalClickingGlove = true;
                player.Clicker().accTriggerFinger = true;
                calClicker.Call("SetAccessoryItem", player, "FingerOfBloodGod", Item);
                calClicker.Call("SetAccessoryItem", player, "LihzahrdParticleAccelerator", Item, 100);

                //SS Medal
                if (player.AddEffect<SMedalEffect>(Item))
                    calClicker.Call("SetAccessoryItem", player, "SSMedal", Item);

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
                if (player.AddEffect<CalamityChocolateChipEffect>(Item))
                    player.Clicker().EnableClickEffect(ClickEffect.ChocolateChip);
                player.Clicker().accCookieItem = Item;
                player.Clicker().accCookie2 = true;
                player.Clicker().accGlassOfMilk = true;

                //AIM Module
                player.Clicker().accAimbotModule = true;
                player.Clicker().accAimbotModule2 = true;

                //Regal Clicking Glove
                player.Clicker().accRegalClickingGlove = true;

                //SMedal
                if (player.AddEffect<SSMedalEffect>(Item))
                    player.Clicker().accSMedalItem = Item;
            }
        }
        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("CalamityClickers", out var calClicker) && ModLoader.HasMod("FargowiltasCrossmod"))
            {
                CreateRecipe()
                    .AddIngredient<GamerEssence>()
                    .AddIngredient(calClicker.Find<ModItem>("DOG").Type)
                    .AddIngredient(calClicker.Find<ModItem>("BloodyChocCookies").Type)
                    .AddIngredient(calClicker.Find<ModItem>("CosmicClickingGlove").Type)
                    .AddIngredient(calClicker.Find<ModItem>("SSMedal").Type)

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
                    .AddIngredient<AimbotModule>()
                    .AddIngredient<ChocolateMilkCookies>()
                    .AddIngredient<RegalClickingGlove>()
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
    public class SMedalEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
        public override int ToggleItemType => ModContent.ItemType<SMedal>();
    }
    public class SSMedalEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
        public override int ToggleItemType => ModLoader.TryGetMod("CalamityClickers", out var mod) ? mod.Find<ModItem>("SSMedal").Type : ModContent.ItemType<SMedal>();
    }
    public class ChocolateChipEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
        public override int ToggleItemType => ModContent.ItemType<ChocolateMilkCookies>();
    }
    public class CalamityChocolateChipEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
        public override int ToggleItemType => ModLoader.TryGetMod("CalamityClickers", out var mod) ? mod.Find<ModItem>("BloodyChocCookies").Type : ModContent.ItemType<SMedal>();
    }
}
