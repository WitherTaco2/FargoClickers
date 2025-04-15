using ClickerClass;
using ClickerClass.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Reflection;
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
        public string BorderTexture
        {
            get
            {
                if (ModContent.RequestIfExists<Texture2D>(Texture + "_Outline", out var _))
                    return Texture + "_Outline";
                return null;
            }
        }
        public sealed override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ClickerSystem.RegisterClickerWeapon(this, BorderTexture);

            if (BorderTexture != null)
            {
                PropertyInfo b = typeof(ClickerSystem).GetProperty("ClickerWeaponBorderTexture", BindingFlags.Static | BindingFlags.NonPublic);
                var v = ((Dictionary<int, string>)b.GetValue(ModContent.GetInstance<ClickerSystem>()));
                v.Add(Item.type, BorderTexture);
                b.SetValue(ModContent.GetInstance<ClickerSystem>(), v);
            }

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
