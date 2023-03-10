using CustomSlot.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AccessibilityMod {
    public class AccessibilityModUI : UIState {
        public enum DisplayType {
            OreTooltips,
            BackgroundWallAvailable
        }

        public DraggableUIPanel Panel { get; private set; }
        public Dictionary<DisplayType, InfoDisplay> InfoElements { get; private set; }
        
        private AccessibilityModConfig config;

        public override void OnInitialize() {
            config = ModContent.GetInstance<AccessibilityModConfig>();

            InfoElements = new Dictionary<DisplayType, InfoDisplay>() {
                { DisplayType.OreTooltips, new(config.ShowOreTooltips, "Ore: {0}") },
                { DisplayType.BackgroundWallAvailable, new(config.ShowBackgroundWallAvailable, "Wall: {0}") }
            };

            Panel = new DraggableUIPanel();

            int lineSpacing = FontAssets.MouseText.Value.LineSpacing;

            Panel.Left.Set(500, 0);
            Panel.Top.Set(500, 0);
            Panel.MinWidth.Set(0, 0);
            Panel.Height.Set(lineSpacing * InfoElements.Count + (lineSpacing / 2), 0);

            int index = 0;

            foreach(InfoDisplay display in InfoElements.Values) {
                display.Left.Set(0, 0);
                display.Top.Set(lineSpacing * index++, 0);
                Panel.Append(display);
            }

            Append(Panel);
        }

        public void SetMinWidth(StyleDimension minWidth) {
            Panel.MinWidth = new StyleDimension(
                minWidth.Pixels + Panel.PaddingLeft + Panel.PaddingRight * 2,
                minWidth.Precent);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            foreach(InfoDisplay display in InfoElements.Values) {
                display.ResetText();
            }
        }

        public void ShowOreTooltip(int type, bool longRange) {
            if(!config.ShowOreTooltips 
                || (longRange && !config.EnableLongRangeTooltips)) {
                return;
            }

            InfoElements[DisplayType.OreTooltips].SetFormattedText(TileID.Search.GetName(type));
        }

        public void ShowBackgroundWallAvailable() {
            if(!config.ShowBackgroundWallAvailable) {
                return;
            }

            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];

            InfoElements[DisplayType.BackgroundWallAvailable].SetFormattedText(
                tile.WallType > 0 
                ? Language.GetTextValue("Mods.AccessibilityMod.Yes") 
                : Language.GetTextValue("Mods.AccessibilityMod.No"));
        }
    }
}
