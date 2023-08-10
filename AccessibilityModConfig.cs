using AccessibilityMod.UI;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AccessibilityMod {
    public class AccessibilityModConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("TooltipOptions")]
        [DefaultValue(false)]
        public bool EnableLongRangeTooltips;

        [DefaultValue(true)]
        public bool ShowOreTooltips;

        [DefaultValue(true)]
        public bool ShowBackgroundWallAvailable;

        [DefaultValue(true)]
        public bool ShowCanGrappleTo;

        [Header("PanelOptions")]
        public bool ShowReorderButtons;

        [DefaultValue(true)]
        public bool AllowDraggingPanel;

        [DefaultValue(false)]
        public bool ResetPanelLocation;

        [DefaultValue(typeof(Color), "44, 57, 105, 178")]
        [ColorNoAlpha]
        public Color PanelBackgroundColor;

        [DefaultValue(typeof(Color), "0, 0, 0, 255")]
        [ColorNoAlpha]
        public Color PanelBorderColor;

        [DefaultValue(typeof(Color), "255, 255, 255, 255")]
        [ColorNoAlpha]
        public Color PanelTextColor;

        public override void OnChanged() {
            if(AccessibilityModSystem.UI == null) {
                return;
            }

            AccessibilityModSystem.UI.Panel.CanDrag = AllowDraggingPanel;
            AccessibilityModSystem.UI.Panel.BackgroundColor = PanelBackgroundColor;
            AccessibilityModSystem.UI.Panel.BorderColor = PanelBorderColor;

            foreach(AccessibilityDisplay display in AccessibilityModSystem.Displays.GetAll()) {
                display.TextElement.TextColor = PanelTextColor;
                display.ChangeAppearance(ShowReorderButtons);
            }

            if(ResetPanelLocation) {
                AccessibilityModSystem.UI.ResetPosition();
                ResetPanelLocation = false;
            }

            AccessibilityModSystem.UI.CreateChildren();
        }
    }
}
