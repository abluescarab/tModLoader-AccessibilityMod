using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AccessibilityMod {
    public class AccessibilityModPlayer : ModPlayer {
        public override void LoadData(TagCompound tag) {
            base.LoadData(tag);
        }

        public override void SaveData(TagCompound tag) {
            base.SaveData(tag);
        }

        public override void PostUpdate() {
            AccessibilityModSystem.UI.ShowBackgroundWallAvailable();
        }
    }
}
