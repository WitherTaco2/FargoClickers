using ClickerClass;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Weapons
{
    public class AcornClicker : BetterClickerWeapon
    {
        public static string Acorn { get; internal set; } = string.Empty;
        public override float Radius => 0.6f;
        public override Color RadiusColor => new Color(116, 133, 47);
        public override int DustType => DustID.WoodFurniture;
        public override void SetStaticDefaultsExtra()
        {
            Acorn = ClickerSystem.RegisterClickEffect(Mod, "Acorn", 6, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
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
                    float speed = 6f;
                    float mag = vector.Length();
                    if (mag > speed)
                    {
                        mag = speed / mag;
                        vector *= mag;
                    }
                    int projIndex = Projectile.NewProjectile(source, pos, vector, ModContent.ProjectileType<Acorn>(), damage, knockBack, player.whoAmI);
                    Main.projectile[projIndex].DamageType = ModContent.GetInstance<ClickerDamage>();
                }
            }, true);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, Acorn);
            SetDust(Item, DustType);

            Item.damage = 6;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 50);

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<TrojanSquirrelBag>(2)
                .AddTile(TileID.Solidifier)
                .DisableDecraft()
                .Register();
        }
    }
}
