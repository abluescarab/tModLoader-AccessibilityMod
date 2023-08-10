using AccessibilityMod.UI;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AccessibilityMod {
    public class AccessibilityModConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.AccessibilityMod.Tooltips.TooltipOptions_Header")]
        [DefaultValue(false)]
        [LabelKeyAttribute("$Mods.AccessibilityMod.Tooltips.EnableLongRangeTooltips_Label")]
        [TooltipKeyAttribute("$Mods.AccessibilityMod.Tooltips.EnableLongRangeTooltips_Tooltip")]
        public bool EnableLongRangeTooltips;

        [DefaultValue(true)]
        [LabelKeyAttribute("$Mods.AccessibilityMod.Tooltips.ShowOreTooltips_Label")]
        [TooltipKeyAttribute("$Mods.AccessibilityMod.Tooltips.ShowOreTooltips_Tooltip")]
        public bool ShowOreTooltips;

        [DefaultValue(true)]
        [LabelKeyAttribute("$Mods.AccessibilityMod.Tooltips.ShowBackgroundWallAvailable_Label")]
        [TooltipKeyAttribute("$Mods.AccessibilityMod.Tooltips.ShowBackgroundWallAvailable_Tooltip")]
        public bool ShowBackgroundWallAvailable;

        [DefaultValue(true)]
        [LabelKeyAttribute("$Mods.AccessibilityMod.Tooltips.ShowCanGrappleTo_Label")]
        [TooltipKeyAttribute("$Mods.AccessibilityMod.Tooltips.ShowCanGrappleTo_Tooltip")]
        public bool ShowCanGrappleTo;

        [Header("$Mods.AccessibilityMod.Panel.PanelOptions_Header")]
        [LabelKeyAttribute("$Mods.AccessibilityMod.Panel.ShowReorderButtons_Label")]
        [TooltipKeyAttribute("$Mods.AccessibilityMod.Panel.ShowReorderButtons_Tooltip")]
        public bool ShowReorderButtons;

        [DefaultValue(true)]
        [LabelKeyAttribute("$Mods.AccessibilityMod.Panel.AllowDraggingPanel_Label")]
        [TooltipKeyAttribute("$Mods.AccessibilityMod.Panel.AllowDraggingPanel_Tooltip")]
        public bool AllowDraggingPanel;

        [DefaultValue(false)]
        [LabelKeyAttribute("$Mods.AccessibilityMod.Panel.ResetPanelLocation_Label")]
        [TooltipKeyAttribute("$Mods.AccessibilityMod.Panel.ResetPanelLocation_Tooltip")]
        public bool ResetPanelLocation;

        [LabelKeyAttribute("$Mods.AccessibilityMod.Panel.PanelBackgroundColor_Label")]
        [DefaultValue(typeof(Color), "44, 57, 105, 178")]
        [ColorNoAlpha]
        public Color PanelBackgroundColor;

        [LabelKeyAttribute("$Mods.AccessibilityMod.Panel.PanelBorderColor_Label")]
        [DefaultValue(typeof(Color), "0, 0, 0, 255")]
        [ColorNoAlpha]
        public Color PanelBorderColor;

        [LabelKeyAttribute("$Mods.AccessibilityMod.Panel.PanelTextColor_Label")]
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
