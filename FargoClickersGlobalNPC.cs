using FargoClickers.Content.Items.Weapons;
using FargowiltasSouls.Content.Bosses.BanishedBaron;
using FargowiltasSouls.Content.Bosses.CursedCoffin;
using FargowiltasSouls.Content.Bosses.Lifelight;
using FargowiltasSouls.Content.Bosses.TrojanSquirrel;
using System.IO;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FargoClickers
{
    public class FargoClickersGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int RGBCounter = 0;
        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(RGBCounter);
        }
        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
        {
            RGBCounter = binaryReader.ReadInt32();
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule mainRule = npcLoot.DefineNormalOnlyDropSet();

            //LeadingConditionRule mainRule = new LeadingConditionRule(new Conditions.NotExpert());
            //npcLoot.Add(mainRule);

            if (npc.type == ModContent.NPCType<TrojanSquirrel>())
            {
                mainRule.Add(ModContent.ItemType<AcornClicker>(), 4);
            }
            if (npc.type == ModContent.NPCType<CursedCoffin>())
            {
                mainRule.Add(ModContent.ItemType<CursedClicker>(), 4);
            }
            if (npc.type == ModContent.NPCType<BanishedBaron>())
            {
                mainRule.Add(ModContent.ItemType<BaronClicker>(), 4);
            }
            if (npc.type == ModContent.NPCType<LifeChallenger>())
            {
                mainRule.Add(ModContent.ItemType<LightClicker>(), 4);
            }
        }
    }
}
