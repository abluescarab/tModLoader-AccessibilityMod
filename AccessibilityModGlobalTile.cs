using Terraria;
using Terraria.ModLoader;

namespace AccessibilityMod {
    public class AccessibilityModGlobalTile : GlobalTile {
        public override void MouseOverFar(int i, int j, int type) {
            Tile tile = Main.tile[i, j];

            if(IsOre(Main.tile[i, j])) {
                AccessibilityModSystem.UI.ShowOreTooltip(type, true);
            }
        }

        public override void MouseOver(int i, int j, int type) {
            Tile tile = Main.tile[i, j];

            if(IsOre(Main.tile[i, j])) {
                AccessibilityModSystem.UI.ShowOreTooltip(type, true);
            }
        }

        private bool IsOre(Tile tile) {
            return SceneMetrics.IsValidForOreFinder(tile);
        }
    }
}
