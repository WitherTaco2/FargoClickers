using ClickerClass.Projectiles;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items
{
    public abstract class BetterClickerProjectile : ClickerProjectile, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";
        //public virtual bool UseInvisibleProjectile => false;
        //public override string Texture => UseInvisibleProjectile ? "CalamityMod/Projectiles/InvisibleProj" : base.Texture;

        public sealed override void SetDefaults()
        {
            base.SetDefaults();

            SetDefaultsExtra();
        }
        public virtual void SetDefaultsExtra()
        {

        }
    }
}
