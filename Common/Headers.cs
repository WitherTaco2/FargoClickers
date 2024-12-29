using FargoClickers.Content.Items.Accessories;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargoClickers.Common
{
    public class MatrixHeader : EnchantHeader
    {
        public override int Item => ModContent.ItemType<ForceOfMatrix>();
        public override float Priority => 0.075f;
    }
}
