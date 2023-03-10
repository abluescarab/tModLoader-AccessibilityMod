using Terraria;
using Terraria.ModLoader;

namespace AccessibilityMod {
    public class AccessibilityModGlobalTile : GlobalTile {
        public override void MouseOver(int i, int j, int type) {
            if(!IsOre(Main.tile[i, j])) {
                return;
            }

            AccessibilityModSystem.UI.ShowOreTooltip(type);
        }

        private bool IsOre(Tile tile) {
            return SceneMetrics.IsValidForOreFinder(tile);
        }
    }
}
