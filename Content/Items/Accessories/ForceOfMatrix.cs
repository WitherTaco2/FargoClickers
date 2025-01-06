using ClickerClass.DrawLayers;
using FargoClickers.Content.Items.Accessories.Enchantments;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.UI.Elements;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace FargoClickers.Content.Items.Accessories
{
    public class ForceOfMatrix : BaseForce
    {
        /*public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }*/
        public static Asset<Texture2D> glowmask;
        public static Texture2D forceTexture;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Enchants[Type] =
            [
                ModContent.ItemType<MotherboardEnchantment>(),
                ModContent.ItemType<RGBEnchantment>(),
                ModContent.ItemType<OverclockEnchantment>(),
                ModContent.ItemType<PrecursorEnchantment>(),
                ModContent.ItemType<MiceEnchantment>(),
            ];
            if (!Main.dedServ)
            {
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
                forceTexture = ModContent.Request<Texture2D>(Texture).Value;

                HeldItemLayer.RegisterData(Item.type, new DrawLayerData()
                {
                    Texture = glowmask,
                    Color = (drawInfo) => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 50) * 0.7f
                });
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MatrixForceEffect>(Item);

            if (player.AddEffect<MiceEffect>(Item))
                player.AddEffect<OverclockEffect>(Item);

            player.AddEffect<MotherboardEffect>(Item);
            player.AddEffect<RGBEffect>(Item);
            player.AddEffect<PrecursorEffect>(Item);


            player.AddEffect<RGBBigRedButtonEffect>(Item);
            player.AddEffect<OverclockBottomlessBoxofPaperclipsEffect>(Item);
            player.AddEffect<PrecursorMasterKeychainEffect>(Item);


        }
        public override void SafePostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            spriteBatch.Draw(
                glowmask.Value,
                new Vector2(
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - glowmask.Value.Height * 0.5f
                ),
                new Rectangle(0, 0, glowmask.Value.Width, glowmask.Value.Height),
                new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 50) * 0.7f,
                rotation,
                glowmask.Value.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f);
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture + "_Glow");
            spriteBatch.Draw(texture.Value, position, new Rectangle?(), Main.DiscoColor, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            foreach (int ench in Enchants[Type])
                recipe.AddIngredient(ench);
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
    public class MatrixForceEffect : AccessoryEffect
    {
        public override Header ToggleHeader => null;
        public override void PostUpdateEquips(Player player)
        {
            FargoClickerPlayer modPlayer = player.FargoClickerPlayer();
            if (player.HasEffect<MiceEffect>())
            {
                modPlayer.MiceEnch = true;

                if (modPlayer.miceHurtTimer > 0)
                    modPlayer.miceHurtTimer--;

                if (modPlayer.miceCooldownTimer > 0 && modPlayer.miceCooldownTimerMax > 0)
                {
                    modPlayer.miceCooldownTimer--;
                    CooldownBarManager.Activate("MiceCooldown", ModContent.Request<Texture2D>("FargoClickers/Content/Items/Accessories/ForceOfMatrix").Value, new Color(98, 101, 145),
                        () => Main.LocalPlayer.FargoClickerPlayer().miceCooldownTimerRatio, true, activeFunction: () => player.HasEffect<MiceEffect>());
                }

                if (modPlayer.matrixBuffTimer > 0)
                {
                    modPlayer.matrixBuffTimer--;
                    player.moveSpeed += 0.15f;
                    player.runAcceleration += 0.15f;
                    CooldownBarManager.Activate("MiceBuff", ModContent.Request<Texture2D>("FargoClickers/Content/Items/Accessories/ForceOfMatrix").Value, new Color(177, 179, 224),
                        () => (float)Main.LocalPlayer.FargoClickerPlayer().matrixBuffTimer / Main.LocalPlayer.FargoClickerPlayer().matrixBuffTimerMax, true, activeFunction: () => player.HasEffect<MiceEffect>());
                }
            }
        }
        public override void OnHurt(Player player, Player.HurtInfo info)
        {
            FargoClickerPlayer modPlayer = Main.LocalPlayer.FargoClickerPlayer();

            if (modPlayer.miceHurtTimer > 0 /*&& !modPlayer.miceHurtTriggered*/)
            {
                player.immuneTime = 180;
                modPlayer.matrixBuffTimer = modPlayer.matrixBuffTimerMax;
                modPlayer.miceCooldownTimer += 59 * 60;
                modPlayer.miceCooldownTimerMax = 3600;
                modPlayer.miceHurtTimer = 0;
            }
        }
        public override float ModifyUseSpeed(Player player, Item item)
        {
            if (player.FargoClickerPlayer().matrixBuffTimer > 0 && player.HasEffect<OverclockEffect>())
                return player.FargoSouls().AttackSpeed;
            return 0;
        }
    }
}
