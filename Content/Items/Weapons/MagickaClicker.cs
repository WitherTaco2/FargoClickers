using ClickerClass;
using ClickerClass.Items.Weapons.Clickers;
using Fargowiltas.Items.Summons.SwarmSummons.Energizers;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Weapons
{
    [LegacyName("MagicaClicker")]
    public class MagickaClicker : BetterClickerWeapon
    {
        public static string MagickaEnchantment { get; internal set; } = string.Empty;
        public static string MagickaPower { get; internal set; } = string.Empty;
        public override float Radius => 6f;
        public override Color RadiusColor => new Color(255, 180, 255);
        public override int DustType => DustID.VenomStaff;
        public override void SetStaticDefaultsExtra()
        {
            MagickaEnchantment = ClickerSystem.RegisterClickEffect(Mod, "MagickaEnchantment", 25, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                SoundEngine.PlaySound(SoundID.Item24, player.Center);
                player.AddBuff(ModContent.BuffType<MagickaClickerBuff>(), 300, false);
                for (int i = 0; i < 15; i++)
                {
                    int index = Dust.NewDust(player.position, player.width, player.height, DustID.VenomStaff, 0f, 0f, 0, Color.White, 2f);
                    Dust dust = Main.dust[index];
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                    int x = Main.rand.Next(-50, 51);
                    int y = Main.rand.Next(-50, 51);
                    dust.position.X += x;
                    dust.position.Y += y;
                    dust.velocity.X = -x * 0.075f;
                    dust.velocity.Y = -y * 0.075f;
                }
            });
            ClickerExtraCompat.RegisterPostWildMagicClickEffect(MagickaEnchantment);

            MagickaPower = ClickerSystem.RegisterClickEffect(Mod, "MagickaPower", 1, RadiusColor, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ProjectileID.PrincessWeapon, damage, knockBack, player.whoAmI);
            });
            ClickerExtraCompat.RegisterPostWildMagicClickEffect(MagickaEnchantment);
        }
        public override void SetDefaultsExtra()
        {
            AddEffect(Item, ClickEffect.ArcaneEnchantment);
            AddEffect(Item, MagickaEnchantment);
            SetDust(Item, DustType);

            Item.damage = 120;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(0, 25);

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ArcaneClicker>()
                .AddIngredient<AbomEnergy>(10)
                .AddIngredient<EnergizerDarkMage>()
                .AddTile<CrucibleCosmosSheet>()
                .DisableDecraft()
                .Register();
        }
    }
    public class MagickaClickerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            ClickerCompat.EnableClickEffect(player, MagickaClicker.MagickaPower);
        }
    }
}
