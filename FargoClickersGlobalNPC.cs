using FargoClickers.Content.Items.Weapons;
using FargowiltasSouls.Content.Bosses.CursedCoffin;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace FargoClickers
{
    public class FargoClickersGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule mainRule = npcLoot.DefineNormalOnlyDropSet();

            //LeadingConditionRule mainRule = new LeadingConditionRule(new Conditions.NotExpert());
            //npcLoot.Add(mainRule);

            if (npc.type == ModContent.NPCType<CursedCoffin>())
            {
                mainRule.Add(ModContent.ItemType<CursedClicker>(), 4);
            }
        }
    }
}
