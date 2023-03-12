using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AccessibilityMod {
    public class InfoDisplay : UIText {
        public bool Visible { get; set; }
        public string Format { get; set; }

        private string translation = "Mods.AccessibilityMod.InfoDisplay_Default";

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
            SetText(string.Format(Format, Language.GetTextValue(translation)));
        }

        public override void Update(GameTime gameTime) {
            if(!Visible) {
                return;
            }

            base.Update(gameTime);

            if(!Text.Contains(translation) 
                && Parent.MinWidth.Pixels < MinWidth.Pixels) {
                AccessibilityModSystem.UI.SetMinWidth(MinWidth);
            }
        }
    }
}
