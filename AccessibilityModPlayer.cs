using CustomSlot;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AccessibilityMod {
    public class AccessibilityModPlayer : ModPlayer {
        private PlayerData<float> panelX = 
            new PlayerData<float>("panelX", AccessibilityModUI.DefaultCoordinates.X);
        private PlayerData<float> panelY = 
            new PlayerData<float>("panelY", AccessibilityModUI.DefaultCoordinates.Y);

        public override void LoadData(TagCompound tag) {
            panelX.Value = tag.GetFloat(panelX.Tag);
            panelY.Value = tag.GetFloat(panelY.Tag);
        }

        public override void SaveData(TagCompound tag) {
            tag.Add(panelX.Tag, AccessibilityModSystem.UI.Panel.Left.Pixels);
            tag.Add(panelY.Tag, AccessibilityModSystem.UI.Panel.Top.Pixels);
        }

        public override void OnEnterWorld(Player player) {
            AccessibilityModSystem.UI.SetPosition(panelX.Value, panelY.Value);
        }

        public override void PostUpdate() {
            AccessibilityModSystem.UI.ShowBackgroundWallAvailable();
        }
    }
}
