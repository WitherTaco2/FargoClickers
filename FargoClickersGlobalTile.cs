using ClickerClass.Buffs;
using ClickerClass.Tiles;
using Fargowiltas;
using Fargowiltas.Common.Configs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers
{
    public class FargoClickersGlobalTile : GlobalTile
    {
        public override void NearbyEffects(int i, int j, int type, bool closer)
        {
            if (FargoServerConfig.Instance.PermanentStationsNearby)
            {
                if (type == ModContent.TileType<DesktopComputerTile>())
                {
                    if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
                    {
                        int buff = ModContent.BuffType<DesktopComputerBuff>();
                        bool noAlchemistNPC = !(ModLoader.HasMod("AlchemistNPC") || ModLoader.HasMod("AlchemistNPCLite")); // because it fucks with buffs for some reason and makes the sound spam WHY WHY WHY WHY WHAT'S WRONG WITH YOU WHY WHY WHY
                        if (!Main.LocalPlayer.HasBuff(buff) && noAlchemistNPC && Main.LocalPlayer.GetModPlayer<FargoPlayer>().StationSoundCooldown <= 0)
                        {
                            SoundEngine.PlaySound(SoundID.Camera, new Vector2(i, j) * 16);
                            Main.LocalPlayer.GetModPlayer<FargoPlayer>().StationSoundCooldown = 60 * 60;
                        }
                        Main.LocalPlayer.AddBuff(buff, 2);
                    }
                }
            }
        }
    }
}
