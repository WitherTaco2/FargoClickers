using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;

namespace FargoClickers
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class FargoClickers : Mod
    {
        public static FargoClickers mod;
        public static Mod extraAPI;
        public override void Load()
        {
            mod = this;
            ModLoader.TryGetMod("ClickerExtraAPI", out extraAPI);

            extraAPI.Call("NerfTheClicker");
            extraAPI.Call("AddTheClickerRecipeIngredient", ModContent.ItemType<EternalEnergy>(), 20);
        }
        public override void Unload()
        {
            mod = null;
        }
    }
}
