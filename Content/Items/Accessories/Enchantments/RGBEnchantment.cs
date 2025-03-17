using ClickerClass;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Armors;
using ClickerClass.Items.Placeable;
using ClickerClass.Items.Weapons.Clickers;
using ClickerClass.Projectiles;
using FargoClickers.Common;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Accessories.Enchantments
{
    public class RGBEnchantment : BaseEnchant
    {
        public override Color nameColor => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ClickerSystem.RegisterClickerItem(this);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Orange;
            Item.value = 10000;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color?(nameColor);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<RGBEffect>(Item);
            player.AddEffect<RGBBigRedButtonEffect>(Item);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<RGBHelm>()
                .AddIngredient<RGBBreastplate>()
                .AddIngredient<RGBGreaves>()

                .AddIngredient<BigRedButton>()
                .AddIngredient<CrystalClicker>()
                .AddIngredient<OutsideTheCave>()

                .AddTile(TileID.CrystalBall)
                .Register();
        }
    }
    public class RGBEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<RGBEnchantment>();
        public override void OnHitNPCWithItem(Player player, Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            OnHitEffect(player, target, item.GetSource_OnHit(target), target.Center, item.damage);
        }
        public override void OnHitNPCWithProj(Player player, Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            OnHitEffect(player, target, proj.GetSource_OnHit(target), proj.Center, proj.damage);
        }
        public void OnHitEffect(Player player, NPC target, IEntitySource source, Vector2 pos, int damage)
        {
            target.GetGlobalNPC<FargoClickersGlobalNPC>().RGBCounter++;
            bool isForce = player.HasEffect<MatrixForceEffect>() || player.ForceEffect<RGBEffect>();
            if (target.GetGlobalNPC<FargoClickersGlobalNPC>().RGBCounter > (isForce ? 75 : 100))
            {
                bool spawnEffects = true;
                int chromatic = ModContent.ProjectileType<RGBPro>();

                float total = 7f;
                int i = 0;
                while (i < total)
                {
                    float hasSpawnEffects = spawnEffects ? 1f : 0f;
                    Vector2 toDir = Vector2.UnitX * 0f;
                    toDir += -Vector2.UnitY.RotatedBy(i * (MathHelper.TwoPi / total)) * new Vector2(10f, 10f);
                    //float damageAmount = (int)(damage);
                    //damageAmount = damageAmount < 1 ? 1 : damageAmount;
                    int index = Projectile.NewProjectile(source, pos, toDir.SafeNormalize(Vector2.UnitY) * 10f, chromatic, damage, 1f, player.whoAmI, 0f, hasSpawnEffects);
                    Main.projectile[index].DamageType = DamageClass.Generic;
                    i++;
                    spawnEffects = false;
                }
                target.GetGlobalNPC<FargoClickersGlobalNPC>().RGBCounter = 0;
            }
        }

    }
    public class RGBBigRedButtonEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<RGBEnchantment>();
        public override void PostUpdateEquips(Player player)
        {
            player.Clicker().EnableClickEffect(ClickEffect.BigRedButton);
        }
    }
}
