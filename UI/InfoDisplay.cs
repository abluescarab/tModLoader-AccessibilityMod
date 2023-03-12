using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AccessibilityMod {
    public class InfoDisplay : UIText {
        private Func<bool> isVisible;

        public string Format { get; set; }
        public bool IsVisible => isVisible();

        private string translation = "Mods.AccessibilityMod.InfoDisplay_Default";

        public InfoDisplay(string format, Func<bool> isVisible) : base("", 1, false) {
            Name = name;
            Format = format;
            this.isVisible = isVisible;
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
            if(!IsVisible) {
                return;
            }

            base.Update(gameTime);

            if(!Text.Contains(translation) 
                && Parent.MinWidth.Pixels < MinWidth.Pixels) {
                AccessibilityModSystem.UI.SetMinWidth(MinWidth);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            if(!IsVisible) {
                return;
            }

            base.DrawSelf(spriteBatch);
        }
    }
}
