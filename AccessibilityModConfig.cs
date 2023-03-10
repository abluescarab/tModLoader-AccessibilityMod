using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AccessibilityMod {
    public class AccessibilityModConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        [Label("$Mods.AccessibilityMod.AllowDraggingPanel_Label")]
        [Tooltip("$Mods.AccessibilityMod.AllowDraggingPanel_Tooltip")]
        public bool AllowDraggingPanel;

        [DefaultValue(true)]
        [Label("$Mods.AccessibilityMod.ShowOreTooltips_Label")]
        [Tooltip("$Mods.AccessibilityMod.ShowOreTooltips_Tooltip")]
        public bool ShowOreTooltips;

        [DefaultValue(true)]
        [Label("$Mods.AccessibilityMod.ShowGemTooltips_Label")]
        [Tooltip("$Mods.AccessibilityMod.ShowGemTooltips_Tooltip")]
        public bool ShowGemTooltips;

        [DefaultValue(true)]
        [Label("$Mods.AccessibilityMod.ShowBackgroundWallAvailable_Label")]
        [Tooltip("$Mods.AccessibilityMod.ShowBackgroundWallAvailable_Tooltip")]
        public bool ShowBackgroundWallAvailable;

        public override void OnChanged() {
            AccessibilityModSystem.UI.Panel.CanDrag = AllowDraggingPanel;
        }
    }
}
