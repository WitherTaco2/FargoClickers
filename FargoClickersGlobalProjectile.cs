using FargoClickers.Content.Items;
using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using Terraria;
using Terraria.ModLoader;

namespace FargoClickers
{
    public class FargoClickersGlobalProjectile : GlobalProjectile
    {
        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if (projectile.type == ModContent.ProjectileType<CosmosForceMoon>() && Main.rand.NextBool(3))
            {
                Item.NewItem(projectile.GetSource_FromThis(), projectile.Hitbox, ModContent.ItemType<MiceBooster>(), noGrabDelay: true);
            }
        }
    }
}
