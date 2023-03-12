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
        public DraggableUIPanel Panel { get; private set; }
        public bool IsVisible { get; set; } = true;
        public static Vector2 DefaultCoordinates => new Vector2(0, 20);

        public override void OnInitialize() {
            Panel = new DraggableUIPanel();

            Panel.HAlign = 0.5f;
            CreateChildren();

            Append(Panel);
        }

        public void SetMinWidth(StyleDimension minWidth) {
            Panel.MinWidth = new StyleDimension(
                minWidth.Pixels + Panel.PaddingLeft * 2 + Panel.PaddingRight * 2,
                minWidth.Precent);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            foreach(AccessibilityDisplay display in AccessibilityModSystem.Displays.GetAll()) {
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

        public void CreateChildren() {
            AccessibilityDisplay[] visibleDisplays = AccessibilityModSystem.Displays.GetVisible(true);
            int displayed = visibleDisplays.Length;
            int lineSpacing = FontAssets.MouseText.Value.LineSpacing;
            int index = 0;

            Panel.RemoveAllChildren();

            Panel.MinWidth.Set(0, 0);
            Panel.Height.Set(
                lineSpacing
                * displayed
                + (lineSpacing / 2), 0);

            foreach(AccessibilityDisplay display in visibleDisplays) {
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

            AccessibilityModSystem.Displays.SetText(AccessibilityDisplays.Defaults.OreTooltips,
                TileID.Search.GetName(type));
        }

        public void ShowBackgroundWallAvailable() {
            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];

            AccessibilityModSystem.Displays.SetText(AccessibilityDisplays.Defaults.BackgroundWallAvailable,
                tile.WallType > 0
                ? Language.GetTextValue("Mods.AccessibilityMod.InfoDisplay_Yes")
                : Language.GetTextValue("Mods.AccessibilityMod.InfoDisplay_No"));
        }
    }
}
