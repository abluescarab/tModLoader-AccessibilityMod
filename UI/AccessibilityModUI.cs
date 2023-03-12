using CustomSlot.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AccessibilityMod.UI {
    public class AccessibilityModUI : UIState {
        public enum DisplayType {
            OreTooltips,
            BackgroundWallAvailable
        }

        public DraggableUIPanel Panel { get; private set; }
        public bool IsVisible { get; set; }
        public static Vector2 DefaultCoordinates => new Vector2(0, 20);

        public override void OnInitialize() {
            Panel = new DraggableUIPanel();
            Panel.HAlign = 0.5f;
            CheckVisibility();

            Append(Panel);
        }

        public void SetMinWidth(StyleDimension minWidth) {
            Panel.MinWidth = new StyleDimension(
                minWidth.Pixels + Panel.PaddingLeft + Panel.PaddingRight * 2,
                minWidth.Precent);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            foreach(InfoDisplay display in InfoDisplays.GetAll()) {
                display.ResetText();
            }
        }

        public void ResetPosition() {
            Panel.Left.Set(DefaultCoordinates.X, 0);
            Panel.Top.Set(DefaultCoordinates.Y, 0);
        }

        public void SetPosition(float x, float y) {
            Panel.Left.Set(x, 0);
            Panel.Top.Set(y, 0);
        }

        public void CheckVisibility() {
            InfoDisplay[] visibleDisplays = InfoDisplays.GetVisible();
            int displayed = visibleDisplays.Length;
            int lineSpacing = FontAssets.MouseText.Value.LineSpacing;
            int index = 0;

            Panel.RemoveAllChildren();

            Panel.MinWidth.Set(0, 0);
            Panel.Height.Set(
                lineSpacing
                * displayed
                + (lineSpacing / 2), 0);

            foreach(InfoDisplay display in visibleDisplays) {
                display.Left.Set(0, 0);
                display.Top.Set(lineSpacing * index++, 0);
                Panel.Append(display);
            }

            if(displayed > 0) {
                IsVisible = true;
            }
            else {
                IsVisible = false;
            }
        }

        public void ShowOreTooltip(int type, bool longRange) {
            AccessibilityModConfig config = ModContent.GetInstance<AccessibilityModConfig>();

            if(longRange && !config.EnableLongRangeTooltips) {
                return;
            }

            InfoDisplays.SetText(InfoDisplays.OreTooltips, TileID.Search.GetName(type));
        }

        public void ShowBackgroundWallAvailable() {
            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];

            InfoDisplays.SetText(InfoDisplays.BackgroundWallAvailable,
                tile.WallType > 0
                ? Language.GetTextValue("Mods.AccessibilityMod.Yes")
                : Language.GetTextValue("Mods.AccessibilityMod.No"));
        }
    }
}
