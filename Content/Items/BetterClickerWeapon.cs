using ClickerClass;
using ClickerClass.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items
{
    public abstract class BetterClickerWeapon : ClickerWeapon, ILocalizedModType, IModType
    {
        public new string LocalizationCategory => "Items.Weapons.Clicker";
        //public static string ClickerEffect { get; internal set; } = string.Empty;
        public override LocalizedText Tooltip => Language.GetOrRegister("Mods.ClickerClass.Common.Tooltips.Clicker");
        public virtual bool SetBorderTexture => false;
        public sealed override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ClickerSystem.RegisterClickerWeapon(this, Texture + "_Outline");
            SetStaticDefaultsExtra();

        }
        public virtual void SetStaticDefaultsExtra()
        {

        }
        public abstract float Radius { get; }
        public abstract Color RadiusColor { get; }
        public abstract int DustType { get; }
        public sealed override void SetDefaults()
        {
            base.SetDefaults();
            ClickerSystem.SetClickerWeaponDefaults(Item);
            ClickerWeapon.SetRadius(Item, Radius);
            ClickerWeapon.SetColor(Item, RadiusColor);
            ClickerWeapon.SetDust(Item, DustType);
            //AddEffect(Item, ClickerEffect);
            Item.width = 30;
            Item.height = 30;

            SetDefaultsExtra();
        }
        public virtual void SetDefaultsExtra()
        {

        }
        public sealed override void UpdateInventory(Player player)
        {
            ClickerWeapon.SetColor(Item, RadiusColor);
            if (DustType > 0)
                ClickerWeapon.SetDust(Item, DustType);
            UpdateInventoryExtra(player);
        }
        public virtual void UpdateInventoryExtra(Player player)
        {

        }
    }
}
