using ClickerClass;
using FargowiltasSouls.Content.Items.BossBags;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Weapons
{
    public class BaronClicker : BetterClickerWeapon
    {
        public static string BanishedMine { get; internal set; } = string.Empty;
        public override float Radius => 3.20f;
        public override Color RadiusColor => new Color(221, 105, 216);
        public override int DustType => DustID.Water;
        public override void SetStaticDefaultsExtra()
        {
            BanishedMine = ClickerSystem.RegisterClickEffect(Mod, "BanishedMine", 8, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                SoundEngine.PlaySound(SoundID.Item17, position);
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<BaronClickerProj>(), damage, knockBack, player.whoAmI);
            });
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, BanishedMine);
            SetDust(Item, DustType);

            Item.damage = 30;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 4);

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<BanishedBaronBag>(2)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
        }
    }
    public class BaronClickerProj : BetterClickerProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaultsExtra()
        {
            Projectile.Size = new Vector2(30);
            Projectile.aiStyle = -1;
            AIType = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = TimeLeft;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 30;
        }
        public const int TimeLeft = 60;
        public Vector2 SpawnCenter = Vector2.Zero;
        public override void OnSpawn(IEntitySource source)
        {
            SpawnCenter = Projectile.Center;
        }
        public override void AI()
        {
            if (Projectile.frameCounter > 9)
            {
                Projectile.frame++;
                Projectile.frame %= 3;
                Projectile.frameCounter = 0;
            }
            Projectile.frameCounter++;

            Projectile.Center = SpawnCenter + new Vector2(0, MathF.Sin(MathF.PI * (Projectile.timeLeft / (float)TimeLeft)) * 100);
            Projectile.rotation += (SpawnCenter - Projectile.Center).Length() / 10f;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            int maxI = 12;
            for (int i = 0; i < maxI; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.UnitX.RotatedBy(MathHelper.TwoPi * (float)i / maxI) * Main.rand.NextFloat(10, 15), ModContent.ProjectileType<BaronClickerProj2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.WriteVector2(SpawnCenter);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            SpawnCenter = reader.ReadVector2();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D t = ModContent.Request<Texture2D>(Texture).Value;
            Main.spriteBatch.Draw(t, Projectile.Center - Main.screenPosition, t.Frame(1, Main.projFrames[Type], 0, Projectile.frame), lightColor, Projectile.rotation, new Vector2(t.Width / 2f, t.Height / Main.projFrames[Type] / 2f), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
    public class BaronClickerProj2 : BetterClickerProjectile
    {
        public override void SetDefaultsExtra()
        {
            Projectile.Size = new Vector2(18);
            Projectile.aiStyle = -1;
            AIType = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 10;
            Projectile.extraUpdates = 1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 30;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = Projectile.damage * 9 / 10;
        }
    }
}
