using ClickerClass;
using ClickerClass.Items.Consumables;
using ClickerClass.Items.Weapons.Clickers;
using FargowiltasSouls.Content.Items.Materials;
using System;
using Terraria;
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
        public override void PostSetupContent()
        {
            double Damage(DamageClass damageClass) => Math.Round(Main.LocalPlayer.GetTotalDamage(damageClass).Additive * Main.LocalPlayer.GetTotalDamage(damageClass).Multiplicative * 100 - 100);
            int Crit(DamageClass damageClass) => (int)Main.LocalPlayer.GetTotalCritChance(damageClass);

            int clickerItem = ModContent.ItemType<CopperClicker>();
            ModLoader.GetMod("Fargowiltas").Call("AddStat", clickerItem, (Func<string>)(() => $"Clicker Damage: {Damage(ModContent.GetInstance<ClickerDamage>())}%"));
            ModLoader.GetMod("Fargowiltas").Call("AddStat", clickerItem, (Func<string>)(() => $"Clicker Critical: {Crit(ModContent.GetInstance<ClickerDamage>())}%"));
            ModLoader.GetMod("Fargowiltas").Call("AddStat", clickerItem, (Func<string>)(() => $"Clicker Radius: {Main.LocalPlayer.Clicker().clickerRadius / 2f * 100 + 50}%"));
            ModLoader.GetMod("Fargowiltas").Call("AddStat", clickerItem, (Func<string>)(() => $"Clicks for Effect: {Main.LocalPlayer.Clicker().clickerBonusPercent * 100}% - {Main.LocalPlayer.Clicker().clickerBonus}"));
            ModLoader.GetMod("Fargowiltas").Call("AddPermaUpgrade", ModContent.GetInstance<HeavenlyChip>().Item, (Func<bool>)(() => Main.LocalPlayer.Clicker().consumedHeavenlyChip));
        }
    }
}
