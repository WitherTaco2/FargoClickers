using ClickerClass;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace FargoClickers
{
    public static class FargoClickersUtils
    {
        public static Player Owner(this Projectile p) => Main.player[p.owner];
        public static Player Owner(this ModProjectile p) => Main.player[p.Projectile.owner];
        public static ClickerPlayer Clicker(this Player player) => player.GetModPlayer<ClickerPlayer>();
        public static FargoClickerPlayer FargoClickerPlayer(this Player p) => p.GetModPlayer<FargoClickerPlayer>();
        public static bool IsCalamityClickersCompatible => ModLoader.TryGetMod("CalamityClickers", out var calClicker) && ModLoader.HasMod("FargowiltasCrossmod");
        public static void RegisterPostWildMagicClickEffect(string clickEffectName)
        {
            if (!(FargoClickers.extraAPI.Call("GetPostWildMagicClickerEffectList") as List<string>).Contains(clickEffectName))
                FargoClickers.extraAPI.Call("RegisterPostWildMagicClickerEffect", clickEffectName);
        }
        public static void RegisterBlacklistedClickEffect(string clickEffectName)
        {
            if (!(FargoClickers.extraAPI.Call("GetBlacklistedRandomClickerEffectList") as List<string>).Contains(clickEffectName))
                FargoClickers.extraAPI.Call("RegisterBlacklistedRandomClickerEffect", clickEffectName);
        }

        public static LeadingConditionRule DefineConditionalDropSet(this ILoot loot, IItemDropRuleCondition condition)
        {
            LeadingConditionRule leadingConditionRule = new LeadingConditionRule(condition);
            loot.Add(leadingConditionRule);
            return leadingConditionRule;
        }
        public static LeadingConditionRule DefineNormalOnlyDropSet(this ILoot loot)
        {
            return loot.DefineConditionalDropSet(new Conditions.NotExpert());
        }
        public static IItemDropRule Add(this LeadingConditionRule mainRule, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1, bool hideLootReport = false)
        {
            return mainRule.OnSuccess(ItemDropRule.Common(itemID, dropRateInt, minQuantity, maxQuantity), hideLootReport);
        }
        public static IItemDropRule Add(this ILoot loot, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1)
        {
            return loot.Add(ItemDropRule.Common(itemID, dropRateInt, minQuantity, maxQuantity));
        }

    }
}
