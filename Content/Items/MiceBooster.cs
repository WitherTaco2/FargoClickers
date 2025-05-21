using ClickerClass.Items;
using FargowiltasSouls.Content.Items.Misc;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items
{
    public class MiceBooster : Booster
    {
        public override string Texture => ModContent.GetInstance<MiceFragment>().Texture;
        public override void PickupEffect(BoosterPlayer boosterPlayer)
        {
            if (boosterPlayer.Player.FargoClickerPlayer().MiceBoosterTimer <= 0)
                CombatText.NewText(boosterPlayer.Player.Hitbox, Color.LightBlue, Language.GetTextValue("Mods.FargoClickers.Items.MiceBooster.Activate", 15), true);
            boosterPlayer.Player.FargoClickerPlayer().MiceBoosterTimer = LunarDuration;
            boosterPlayer.Player.AddBuff(ModContent.BuffType<MiceBoosterBuff>(), LunarDuration);
        }
    }
    public class MiceBoosterBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = player.FargoClickerPlayer().MiceBoosterTimer;
        }
    }
}
