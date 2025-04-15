using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Misc;
using ClickerClass.Items.Weapons.Clickers;
using FargoClickers.Content.Items.Accessories;
using Fargowiltas.Utilities;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Materials;
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
                if (recipe.HasResult<ChaliceoftheMoon>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<DeviatingEnergy>());
                    recipe.AddIngredient<MiceFragment>();
                    recipe.AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 10);
                }
                if (recipe.HasResult<UniverseSoul>())
                {
                    recipe.requiredItem.Insert(4, new Item(ModContent.ItemType<MasterPlayerSoul>()));
                }
                if (recipe.HasResult<TerrariaSoul>())
                {
                    recipe.requiredItem.Insert(4, new Item(ModContent.ItemType<ForceOfMatrix>()));
                }
            }
            //Biome Key Clicker
            Recipe.Create(ModContent.ItemType<MouseClicker>())
                .AddIngredient<DungeonKey>()
                .AddTile(TileID.MythrilAnvil)
                .AddCondition(Condition.DownedPlantera)
                .DisableDecraft()
                .Register();

            //Boss Bag
            Recipe.Create(ModContent.ItemType<BurningSuperDeathClicker>())
                .AddIngredient(ItemID.WallOfFleshBossBag)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<CyclopsClicker>())
                .AddIngredient(ItemID.DeerclopsBossBag)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<DraconicClicker>())
                .AddIngredient(ItemID.BossBagBetsy)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<RainbowClicker>())
                .AddIngredient(ItemID.FairyQueenBossBag)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<SeafoamClicker>())
                .AddIngredient(ItemID.FishronBossBag)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<LordsClicker>())
                .AddIngredient(ItemID.MoonLordBossBag)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();

            Recipe.Create(ModContent.ItemType<BottomlessBoxofPaperclips>())
                .AddIngredient(ItemID.TwinsBossBag)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<BottomlessBoxofPaperclips>())
                .AddIngredient(ItemID.DestroyerBossBag)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<BottomlessBoxofPaperclips>())
                .AddIngredient(ItemID.SkeletronPrimeBossBag)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<ClearKeychain>())
                .AddIngredient(ItemID.QueenSlimeBossBag)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<StickyKeychain>())
                .AddIngredient(ItemID.KingSlimeBossBag)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();

            //Crate
            Recipe.Create(ModContent.ItemType<EnchantedLED>())
                .AddIngredient(ItemID.GoldenCrate, 2)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<EnchantedLED>())
                .AddIngredient(ItemID.GoldenCrateHard, 2)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<IcePack>())
                .AddIngredient(ItemID.FrozenCrate)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<IcePack>())
                .AddIngredient(ItemID.FrozenCrateHard)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<UmbralClicker>())
                .AddIngredient(ItemID.LavaCrate, 3)
                .AddIngredient(ItemID.ShadowKey)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<UmbralClicker>())
                .AddIngredient(ItemID.LavaCrateHard, 3)
                .AddIngredient(ItemID.ShadowKey)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<HotKeychain>())
                .AddIngredient(ItemID.LavaCrate, 5)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<HotKeychain>())
                .AddIngredient(ItemID.LavaCrateHard, 5)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<SlickClicker>())
                .AddIngredient(ItemID.DungeonFishingCrate, 3)
                .AddIngredient(ItemID.GoldenKey)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<SlickClicker>())
                .AddIngredient(ItemID.DungeonFishingCrateHard, 3)
                .AddIngredient(ItemID.GoldenKey)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<StarryClicker>())
                .AddIngredient(ItemID.FloatingIslandFishingCrate, 3)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<StarryClicker>())
                .AddIngredient(ItemID.FloatingIslandFishingCrateHard, 3)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<PharaohsClicker>())
                .AddIngredient(ItemID.OasisCrate, 5)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<PharaohsClicker>())
                .AddIngredient(ItemID.OasisCrateHard, 5)
                .AddTile(TileID.WorkBenches)
                .DisableDecraft()
                .Register();

            //Banner
            /*Recipe.Create(ModContent.ItemType<GoldenTicket>())
                .AddRecipeGroup("Fargowiltas:AnyPirateBanner", 2)
                .AddTile(TileID.Solidifier)
                .Register();*/
            /*Recipe.Create(ModContent.ItemType<CaptainsClicker>())
                .AddRecipeGroup("Fargowiltas:AnyPirateBanner", 2)
                .AddTile(TileID.Solidifier)
                .Register();*/
            Recipe.Create(ModContent.ItemType<Milk>())
                .AddIngredient(ItemID.SkeletonMageBanner)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<ChocolateChip>())
                .AddIngredient(ItemID.GastropodBanner)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<AimAssistModule>())
                .AddIngredient(ItemID.MimicBanner)
                .AddTile(TileID.Solidifier)
                .AddCondition(Condition.Hardmode)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<ImpishClicker>())
                .AddIngredient(ItemID.FireImpBanner)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<EclipticClicker>())
                .AddIngredient(ItemID.FrankensteinBanner)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<EclipticClicker>())
                .AddIngredient(ItemID.SwampThingBanner)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<MyceliumClicker>())
                .AddIngredient(ItemID.SporeSkeletonBanner)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<CaptainsClicker>())
                .AddIngredient(ItemID.PirateCaptainBanner)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<TriggerFinger>())
                .AddIngredient(ItemID.ZombieBanner)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<DungeonKey>())
                .AddRecipeGroup(AnyDungeonBanner, 10)
                .AddTile(TileID.Solidifier)
                .AddCondition(Condition.Hardmode)
                .DisableDecraft()
                .Register();

            //Trophy
            Recipe.Create(ModContent.ItemType<WitchClicker>())
                .AddIngredient(ItemID.MourningWoodTrophy)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<LanternClicker>())
                .AddIngredient(ItemID.PumpkingTrophy)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<NaughtyClicker>())
                .AddIngredient(ItemID.SantaNK1Trophy)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<FrozenClicker>())
                .AddIngredient(ItemID.IceQueenTrophy)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<HighTechClicker>())
                .AddIngredient(ItemID.MartianSaucerTrophy)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<GoldenTicket>())
                .AddIngredient(ItemID.FlyingDutchmanTrophy)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();

            //Other
            Recipe.Create(ModContent.ItemType<ButtonMasher>())
                .AddIngredient(ModLoader.GetMod("Fargowiltas"), "TravellingMerchant")
                .AddIngredient(ItemID.GoldCoin, 10)
                .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<Soda>())
                .AddIngredient(ModLoader.GetMod("Fargowiltas"), "TravellingMerchant")
                .AddIngredient(ItemID.GoldCoin, 2)
                .AddTile(TileID.TinkerersWorkbench)
                .DisableDecraft()
                .Register();
            Recipe.Create(ModContent.ItemType<UmbralClicker>())
                .AddIngredient(ItemID.TreasureMagnet)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
        }
        private static int AnyDungeonBanner;
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => RecipeHelper.GenerateAnyBannerRecipeGroupText("RandomWorldName_Location.Dungeon"),
                ItemID.AngryBonesBanner, 1682, ItemID.CursedSkullBanner, ItemID.DungeonSlimeBanner,
                ItemID.BlueArmoredBonesBanner, ItemID.HellArmoredBonesBanner, ItemID.RustyArmoredBonesBanner,
                ItemID.NecromancerBanner, ItemID.RaggedCasterBanner, ItemID.DiablolistBanner,
                ItemID.SkeletonCommandoBanner, ItemID.SkeletonSniperBanner, ItemID.TacticalSkeletonBanner,
                ItemID.PaladinBanner, ItemID.BoneLeeBanner, ItemID.DungeonSpiritBanner
            );
            AnyDungeonBanner = RecipeGroup.RegisterGroup("FargoClickers:AnyDungeonBanner", group);
        }
    }
}
