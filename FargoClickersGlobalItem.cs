using FargoClickers.Content.Items.Weapons;
using FargowiltasSouls.Content.Items.BossBags;
using Terraria;
using Terraria.ModLoader;

namespace FargoClickers
{
    public class FargoClickersGlobalItem : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ModContent.ItemType<CursedCoffinBag>())
            {
                itemLoot.Add(ModContent.ItemType<CursedClicker>(), 4);
            }
            if (item.type == ModContent.ItemType<LifelightBag>())
            {
                itemLoot.Add(ModContent.ItemType<LightClicker>(), 4);
            }
        }
    }
}
