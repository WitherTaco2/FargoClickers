using ClickerClass;
using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.CursedCoffin;
using FargowiltasSouls.Content.Items.BossBags;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Weapons
{
    public class CursedClicker : BetterClickerWeapon
    {
        public static string Sandwave { get; internal set; } = string.Empty;
        public override float Radius => 2f;
        public override Color RadiusColor => new Color(124, 81, 60);
        public override int DustType => DustID.Sand;
        public override void SetStaticDefaultsExtra()
        {
            Sandwave = ClickerSystem.RegisterClickEffect(Mod, "Sandwave", 8, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Vector2 pos = position;

                int index = -1;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.CanBeChasedBy() && npc.DistanceSQ(pos) < 400f * 400f && Collision.CanHit(pos, 1, 1, npc.Center, 1, 1))
                    {
                        index = i;
                    }
                }
                if (index != -1)
                {
                    Vector2 vector = Main.npc[index].Center - pos;
                    float speed = 3f;
                    float mag = vector.Length();
                    if (mag > speed)
                    {
                        mag = speed / mag;
                        vector *= mag;
                    }
                    Projectile.NewProjectile(source, pos, vector, ModContent.ProjectileType<CursedClickerProjectile>(), damage, knockBack, player.whoAmI);
                }
            }, true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, Sandwave);
            SetDust(Item, DustType);

            Item.damage = 6;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 2);

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedCoffinBag>()
                .AddTile(TileID.Solidifier)
                .Register();
        }
    }
    //CoffinSlamShockwave
    public class CursedClickerProjectile : CoffinSlamShockwave, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Clicker";
        public override string Texture => ModContent.GetInstance<CoffinSlamShockwave>().Texture;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.hide = false;
            Projectile.DamageType = ModContent.GetInstance<ClickerDamage>();
        }
        public override void AI()
        {
            int ticksPerFrame = (int)Math.Round(12f - MathHelper.Clamp(6f * Projectile.velocity.X / 60f, 0f, 6f));
            Projectile.Animate(ticksPerFrame);
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Math.Abs(Math.Sqrt(Math.Pow(Projectile.velocity.X, 2) + Math.Pow(Projectile.velocity.Y, 2))) < 15f)
            {
                Projectile.velocity *= 1.035f;
            }
        }
    }
}
