using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using static AccessibilityMod.AccessibilityModConfig;

namespace AccessibilityMod {
    public class InfoDisplay : UIText {
        public bool Visible { get; set; }
        public string Format { get; set; }
        public Vector2 Position {
            get {
                return GetOuterDimensions().Position();
            }
            set {
                Left.Set(value.X, 0);
                Top.Set(value.Y, 0);
            }
        }

        public InfoDisplay(bool visible, string format) : base("", 1, false) {
            Visible = visible;
            Format = format;
            TextColor = ModContent.GetInstance<AccessibilityModConfig>().PanelTextColor;
            ResetText();
        }

        public void SetFormattedText(string text) {
            SetText(string.Format(Format, text));
        }

        public void ResetText() {
            SetText(string.Format(Format, 
                Language.GetTextValue("Mods.AccessibilityMod.InfoDisplay_Default")));
        }

        public override void Update(GameTime gameTime) {
            if(!Visible) {
                return;
            }

            base.Update(gameTime);

            if(Parent.MinWidth.Pixels < MinWidth.Pixels) {
                AccessibilityModSystem.UI.SetMinWidth(MinWidth);
            }
        }
    }
}
