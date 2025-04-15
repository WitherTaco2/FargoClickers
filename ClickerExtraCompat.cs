using System.Collections.Generic;
using Terraria.ModLoader;

namespace FargoClickers
{
    public class ClickerExtraCompat : ModSystem
    {
        private static Mod clickerExtraAPI;

        internal static Mod ClickerExtraAPI
        {
            get
            {
                if (clickerExtraAPI == null && ModLoader.TryGetMod("ClickerExtraAPI", out var mod))
                {
                    clickerExtraAPI = mod;
                }
                return clickerExtraAPI;
            }
        }
        public override void Unload()
        {
            clickerExtraAPI = null;
        }
        public static void RegisterPostWildMagicClickEffect(string clickEffectName)
        {
            if (!(ClickerExtraAPI.Call("GetPostWildMagicClickerEffectList") as List<string>).Contains(clickEffectName))
                ClickerExtraAPI.Call("RegisterPostWildMagicClickerEffect", clickEffectName);
        }
        public static void RegisterBlacklistedClickEffect(string clickEffectName)
        {
            if (!(ClickerExtraAPI.Call("GetBlacklistedRandomClickerEffectList") as List<string>).Contains(clickEffectName))
                ClickerExtraAPI.Call("RegisterBlacklistedRandomClickerEffect", clickEffectName);
        }

    }
}
