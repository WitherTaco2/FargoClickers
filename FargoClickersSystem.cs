using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Misc;
using ClickerClass.Items.Weapons.Clickers;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Armor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers
{
    public class FargoClickersSystem : ModSystem
    {
        public override void AddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                /*if (ModLoader.TryGetMod("CalamityClickers", out var _))
                {
                    if (recipe.HasResult<TheClicker>())
                    {
                        recipe.AddIngredient<EssenceofBright>(20);
                    }
                }*/
                if (recipe.HasResult<EridanusHat>() || recipe.HasResult<EridanusBattleplate>() || recipe.HasResult<EridanusLegwear>())
                {
                    recipe.AddIngredient<MiceFragment>(5);
                }
                if (recipe.HasResult<GalacticGlobe>())
                {
                    recipe.AddIngredient<MiceFragment>(1);
                }
            }
            //Biome Key Clicker
            Recipe.Create(ModContent.ItemType<MouseClicker>())
                .AddIngredient<DungeonKey>()
                .AddIngredient(ItemID.TempleKey)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            //Boss Bag to Craft
            Recipe.Create(ModContent.ItemType<ClickerEmblem>())
                .AddIngredient(ItemID.WallOfFleshBossBag)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<BurningSuperDeathClicker>())
                .AddIngredient(ItemID.WallOfFleshBossBag)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<CyclopsClicker>())
                .AddIngredient(ItemID.DeerclopsBossBag)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<BurningSuperDeathClicker>())
                .AddIngredient(ItemID.GolemBossBag)
                .AddTile(TileID.Solidifier)
                .Register();

            Recipe.Create(ModContent.ItemType<DraconicClicker>())
                .AddIngredient(ItemID.BossBagBetsy)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<RainbowClicker>())
                .AddIngredient(ItemID.FairyQueenBossBag)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<SeafoamClicker>())
                .AddIngredient(ItemID.FishronBossBag)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<LordsClicker>())
                .AddIngredient(ItemID.FishronBossBag)
                .AddTile(TileID.Solidifier)
                .Register();


            Recipe.Create(ModContent.ItemType<BottomlessBoxofPaperclips>())
                .AddIngredient(ItemID.TwinsBossBag)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<BottomlessBoxofPaperclips>())
                .AddIngredient(ItemID.DestroyerBossBag)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<BottomlessBoxofPaperclips>())
                .AddIngredient(ItemID.SkeletronPrimeBossBag)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<ClearKeychain>())
                .AddIngredient(ItemID.QueenSlimeBossBag)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<StickyKeychain>())
                .AddIngredient(ItemID.KingSlimeBossBag)
                .AddTile(TileID.Solidifier)
                .Register();

            //Crate to craft
            Recipe.Create(ModContent.ItemType<EnchantedLED>())
                .AddIngredient(ItemID.GoldenCrate)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<EnchantedLED>())
                .AddIngredient(ItemID.GoldenCrateHard)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<IcePack>())
                .AddIngredient(ItemID.FrozenCrate)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<IcePack>())
                .AddIngredient(ItemID.FrozenCrateHard)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<UmbralClicker>())
                .AddIngredient(ItemID.LavaCrate)
                .AddIngredient(ItemID.ShadowKey)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<UmbralClicker>())
                .AddIngredient(ItemID.LavaCrateHard)
                .AddIngredient(ItemID.ShadowKey)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<HotKeychain>())
                .AddIngredient(ItemID.LavaCrate, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<HotKeychain>())
                .AddIngredient(ItemID.LavaCrateHard, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<SlickClicker>())
                .AddIngredient(ItemID.DungeonFishingCrate)
                .AddIngredient(ItemID.GoldenKey)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<SlickClicker>())
                .AddIngredient(ItemID.DungeonFishingCrateHard)
                .AddIngredient(ItemID.GoldenKey)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<StarryClicker>())
                .AddIngredient(ItemID.FloatingIslandFishingCrate)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ModContent.ItemType<StarryClicker>())
                .AddIngredient(ItemID.FloatingIslandFishingCrateHard)
                .AddTile(TileID.WorkBenches)
                .Register();

            //Banner to craft
            Recipe.Create(ModContent.ItemType<GoldenTicket>())
                .AddRecipeGroup("Fargowiltas:AnyPirateBanner", 2)
                .AddTile(TileID.Solidifier)
                .Register();
            /*Recipe.Create(ModContent.ItemType<CaptainsClicker>())
                .AddRecipeGroup("Fargowiltas:AnyPirateBanner", 2)
                .AddTile(TileID.Solidifier)
                .Register();*/
            Recipe.Create(ModContent.ItemType<Milk>())
                .AddIngredient(ItemID.SkeletonMageBanner)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<ChocolateChip>())
                .AddIngredient(ItemID.GastropodBanner)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<AimAssistModule>())
                .AddIngredient(ItemID.FireImpBanner)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<ImpishClicker>())
                .AddIngredient(ItemID.FireImpBanner)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<EclipticClicker>())
                .AddIngredient(ItemID.FrankensteinBanner)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<EclipticClicker>())
                .AddIngredient(ItemID.SwampThingBanner)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<MyceliumClicker>())
                .AddIngredient(ItemID.SporeSkeletonBanner)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<CaptainsClicker>())
                .AddIngredient(ItemID.PirateCaptainBanner)
                .AddTile(TileID.Solidifier)
                .Register();

            //Trophy
            Recipe.Create(ModContent.ItemType<WitchClicker>())
                .AddIngredient(ItemID.MourningWoodTrophy)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<LanternClicker>())
                .AddIngredient(ItemID.PumpkingTrophy)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<NaughtyClicker>())
                .AddIngredient(ItemID.SantaNK1Trophy)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<FrozenClicker>())
                .AddIngredient(ItemID.IceQueenTrophy)
                .AddTile(TileID.Solidifier)
                .Register();
            Recipe.Create(ModContent.ItemType<HighTechClicker>())
                .AddIngredient(ItemID.MartianSaucerTrophy)
                .AddTile(TileID.Solidifier)
                .Register();

            //Other
            Recipe.Create(ModContent.ItemType<ButtonMasher>())
                .AddIngredient(ModLoader.GetMod("Fargowiltas"), "TravellingMerchant")
                .AddIngredient(ItemID.GoldCoin, 10)
                .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ModContent.ItemType<UmbralClicker>())
                .AddIngredient(ItemID.TreasureMagnet)
                .AddTile(TileID.Solidifier)
                .Register();

        }
    }
}
