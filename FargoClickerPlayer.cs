using Terraria.ModLoader;

namespace FargoClickers
{
    public class FargoClickerPlayer : ModPlayer
    {
        public bool precursorEnch = false;
        //public Vector2[] precursorPos = new Vector2[10] { new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0) };

        public override void ResetEffects()
        {
            precursorEnch = false;
        }
    }
}
