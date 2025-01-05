using ClickerClass;
using FargowiltasSouls.Content.Bosses.Lifelight;
using FargowiltasSouls.Content.Items.BossBags;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Weapons
{
    public class LightClicker : BetterClickerWeapon
    {
        public static string Lightbomb { get; internal set; } = string.Empty;
        public override float Radius => 3.5f;
        public override Color RadiusColor => Color.Pink;
        public override int DustType => DustID.GemTopaz;
        public override void SetStaticDefaultsExtra()
        {
            Lightbomb = ClickerSystem.RegisterClickEffect(Mod, "Lightbomb", 15, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                for (int i = 0; i < 10; i++)
                    Projectile.NewProjectile(source, position, Main.rand.NextVector2Circular(10, 10), ModContent.ProjectileType<LightClickerProjectile>(), damage, knockBack, player.whoAmI);
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, Lightbomb);
            SetDust(Item, DustType);

            Item.damage = 36;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 2);

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<LifelightBag>()
                .AddTile(TileID.Solidifier)
                .Register();
        }
    }
    public class LightClickerProjectile : LifeBomb, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";
        public override string Texture => ModContent.GetInstance<LifeBomb>().Texture;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.hostile = false;
            Projectile.friendly = true;
            //Projectile.hide = false;
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 30;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            //if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2, 0f, 0f, ModContent.ProjectileType<LightClickerProjectile2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
        }
        //public override bool? CanDamage() => null;
    }
    public class LightClickerProjectile2 : LifeBombExplosion, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";
        public override string Texture => ModContent.GetInstance<LifeBombExplosion>().Texture;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.hostile = false;
            Projectile.friendly = true;
            //Projectile.hide = false;
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 30;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            Projectile.rotation += 2f;
            if (Main.rand.NextBool(6))
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 0.5f;
            }

            //pulsate
            if (Projectile.localAI[0] == 0)
                Projectile.localAI[0] += Main.rand.Next(60);
            Projectile.scale = 1.1f + 0.1f * (float)Math.Sin(MathHelper.TwoPi / 15 * ++Projectile.localAI[1]);

            if (Projectile.ai[0] > MaxTime - 30)
            {
                Projectile.alpha += 8;
                if (Projectile.alpha > 255)
                    Projectile.alpha = 255;
            }

            if (Projectile.ai[0] > MaxTime)
            {
                for (int i = 0; i < 20; i++)
                {
                    int d2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz);
                    Main.dust[d2].noGravity = true;
                    Main.dust[d2].velocity *= 0.5f;
                }
                Projectile.Kill();
            }
            Projectile.ai[0] += 1f;
        }
    }
}
