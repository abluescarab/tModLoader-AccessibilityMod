using CustomSlot.UI;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace AccessibilityMod {
    public class AccessibilityModUI : UIState {
        public DraggableUIPanel Panel { get; private set; }
        public List<UIElement> InfoElements { get; }

        private AccessibilityModConfig config;

        public AccessibilityModUI() {
            InfoElements = new List<UIElement>();
        }

        public override void OnInitialize() {
            config = ModContent.GetInstance<AccessibilityModConfig>();
            Panel = new DraggableUIPanel();

            Panel.Width.Set(FontAssets.MouseText.Value.CharacterSpacing * 100, 0);
            Panel.Height.Set(FontAssets.MouseText.Value.LineSpacing * 5, 0);

            Append(Panel);
        }

        public void ShowOreTooltip(int type) {
            if(!config.ShowOreTooltips) {
                return;
            }

            // TODO
            //Main.instance.MouseText(TileID.Search.GetName(type));
            //Main.mouseText = true;
        }

        public void ShowGemTooltip(int type) {
            if(!config.ShowGemTooltips) {
                return;
            }

            // TODO
        }

        public void ShowBackgroundWallAvailable() {
            if(!config.ShowBackgroundWallAvailable) {
                return;
            }

            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];

            if(tile.WallType > WallID.None) {
                // TODO
                Main.instance.MouseText("Wall available");
                Main.mouseText = true;
            }
        }
    }
}
