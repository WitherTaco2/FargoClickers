using FargoClickers.Content.Items.Accessories;
using FargoClickers.Content.Items.Weapons;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.BossBags;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargoClickers
{
    public class FargoClickersGlobalItem : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ModContent.ItemType<TrojanSquirrelBag>())
            {
                itemLoot.Add(ModContent.ItemType<AcornClicker>(), 4);
            }
            if (item.type == ModContent.ItemType<CursedCoffinBag>())
            {
                itemLoot.Add(ModContent.ItemType<CursedClicker>(), 4);
            }
            if (item.type == ModContent.ItemType<BanishedBaronBag>())
            {
                itemLoot.Add(ModContent.ItemType<BaronClicker>(), 4);
            }
            if (item.type == ModContent.ItemType<LifelightBag>())
            {
                itemLoot.Add(ModContent.ItemType<LightClicker>(), 4);
            }
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.ModItem is UniverseSoul or EternitySoul)
            {
                player.Clicker().clickerRadius += 2f;
                player.Clicker().clickerBonusPercent += 0.2f;
                MasterPlayerSoul.UpdateMasterPlayerSoulAccessories(item, player, hideVisual);
            }
            if (item.ModItem is TerrariaSoul or EternitySoul)
            {
                ForceOfMatrix.UpdateForceOfMatrix(player, item);
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<UniverseSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(Mod, "ClickStatUniverseSoul", Language.GetTextValue("Mods.FargoClickers.ExpandedTooltips.ClickerRadius") + "\n"
                                                                               + Language.GetTextValue("Mods.FargoClickers.ExpandedTooltips.ClickerEffect")));
                tooltips.Insert(15, new TooltipLine(Mod, "ClickAccUniverseSoul", (ModLoader.HasMod("CalamityClickers") && ModLoader.HasMod("FargowiltasCrossmod")) ? Language.GetTextValue("Mods.FargoClickers.Items.MasterPlayerSoul.CalamityAccessories") : Language.GetTextValue("Mods.FargoClickers.Items.MasterPlayerSoul.NormalAccessories")));
            }
            if (item.type == ModContent.ItemType<TerrariaSoul>())
            {
                tooltips.Insert(23, new TooltipLine(Mod, "ClickStatUniverseSoul", Language.GetTextValue("Mods.FargoClickers.ExpandedTooltips.MatrixForce")));
            }
        }
    }
}
