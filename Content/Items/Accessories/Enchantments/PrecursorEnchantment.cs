using ClickerClass;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Armors;
using ClickerClass.Items.Weapons.Clickers;
using FargoClickers.Common;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Accessories.Enchantments
{
    public class PrecursorEnchantment : BaseEnchant
    {
        public override Color nameColor => new Color(255, 197, 35);
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ClickerSystem.RegisterClickerItem(this);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Yellow;
            Item.value = 10000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MasterKeychainEffect>(Item);
            player.AddEffect<PrecursorEffect>(Item);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<PrecursorHelmet>()
                .AddIngredient<PrecursorBreastplate>()
                .AddIngredient<PrecursorGreaves>()

                .AddIngredient<MasterKeychain>()
                .AddIngredient<LihzahrdClicker>()
                .AddIngredient<ChlorophyteClicker>()

                .AddTile(TileID.CrystalBall)
                .Register();
        }
    }
    public class MasterKeychainEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<PrecursorEnchantment>();
        public override void PostUpdateEquips(Player player)
        {
            ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
            clickerPlayer.accHotKeychain = true;
            clickerPlayer.EnableClickEffect(ClickEffect.ClearKeychain);
            clickerPlayer.EnableClickEffect(ClickEffect.StickyKeychain);
        }
    }
    public class PrecursorEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<MatrixHeader>();
        public override int ToggleItemType => ModContent.ItemType<PrecursorEnchantment>();
        public override void PostUpdateEquips(Player player)
        {
            int type = ModContent.ProjectileType<PrecursorProjectile>();

            player.FargoClickerPlayer().PrecursorEnch = true;
            if (player.ownedProjectileCounts[type] < 1)
                Projectile.NewProjectile(player.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, type, 100, 1f, player.whoAmI);
        }
    }
    public class PrecursorProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            AIType = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            //Projectile.oldPos
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 30;
        }
        public int PrecursorLenghtFactor(Player player) => player.FargoSouls().ForceEffect<PrecursorEnchantment>() ? 1 : 2;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active)
            {
                Projectile.active = false;
                return;
            }
            if (player.dead || player.ghost)
            {
                player.FargoClickerPlayer().PrecursorEnch = false;
            }
            if (player.FargoClickerPlayer().PrecursorEnch)
            {
                Projectile.timeLeft = 2;
            }

            Projectile.Center = Main.MouseWorld;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
                Projectile.oldPos[i] += player.velocity;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= this.Owner().FargoSouls().ForceEffect<PrecursorEnchantment>() ? 2 : 1;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            for (int i = 0; i < Projectile.oldPos.Length / PrecursorLenghtFactor(player); i++)
            {
                Vector2 oldPosition = Projectile.oldPos[i];
                //Rectangle 

                //Rectangle rect = new((int)oldPosition.X, (int)oldPosition.Y, 1, 1);
                Rectangle rect = projHitbox;
                rect.X = (int)oldPosition.X;
                rect.Y = (int)oldPosition.Y;

                if (rect.Intersects(targetHitbox))
                    return true;

                /*foreach (NPC npc in Main.npc)
                {
                    if (npc == null) continue;
                    if (npc.active && npc.life > 0 && rect.Intersects(targetHitbox))
                    {
                        return true;
                    }
                }*/
            }
            return base.Colliding(projHitbox, targetHitbox);
        }
        /*public override bool? CanHitNPC(NPC target)
        {
            for (int i = 0; i < Projectile.oldPos.Length / (Main.player[Projectile.owner].FargoSouls().WizardEnchantActive ? 2 : 1); i++)
            {
                if (Collision.CheckAABBvAABBCollision(Projectile.oldPos[i], Projectile.Size, target.position, target.Size))
                    return true;
            }
            return base.CanHitNPC(target);
        }*/
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                Texture2D t = TextureAssets.Projectile[Projectile.type].Value;
                Vector2 origin = Projectile.Size / 2f;

                int afterimageCount = ProjectileID.Sets.TrailCacheLength[Projectile.type] / PrecursorLenghtFactor(Main.player[Projectile.owner]);
                int k = 0;
                while (k < afterimageCount)
                {
                    Vector2 drawPos = Projectile.oldPos[k] + origin - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                    Main.spriteBatch.Draw(t, drawPos, null, lightColor, 0, origin, Projectile.scale, SpriteEffects.None, 0f);
                    k++;
                }
                return base.PreDraw(ref lightColor);
            }
            return false;
        }
    }
}
