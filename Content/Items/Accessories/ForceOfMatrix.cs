using ClickerClass.DrawLayers;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
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
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Enchants[Type] =
            [
            ModContent.ItemType<PrecursorEnchantment>()
            ];
            if (!Main.dedServ)
            {
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

                HeldItemLayer.RegisterData(Item.type, new DrawLayerData()
                {
                    Texture = glowmask,
                    Color = (drawInfo) => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 50) * 0.7f
                });
            }
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
    }
    public class MatrixForceEffect : AccessoryEffect
    {
        public override Header ToggleHeader => null;

    }
}
