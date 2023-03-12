using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AccessibilityMod.UI {
    public class AccessibilityDisplay : UIElement {
        private const int ElementMargin = 5;

        private Func<bool> isVisible;

        public UIText TextElement;
        public UIUpDownButton ChangeOrderButton;

        public string Name { get; }
        public string Format { get; }
        public int Order { get; set; }
        public bool IsVisible => isVisible();

        private string translation = "Mods.AccessibilityMod.InfoDisplay_Default";

        public AccessibilityDisplay(string name, string format, Func<bool> isVisible, int order) {
            Name = name;
            Format = format;
            this.isVisible = isVisible;
            Order = order;

            AccessibilityModConfig config = ModContent.GetInstance<AccessibilityModConfig>();
            ChangeOrderButton = new UIUpDownButton();

            ChangeOrderButton.Left.Set(0, 0);
            ChangeOrderButton.Top.Set(0, 0);
            ChangeOrderButton.OnClickUpButton += MoveUp;
            ChangeOrderButton.OnClickDownButton += MoveDown;

            TextElement = new UIText("");

            TextElement.VAlign = 0.5f;
            TextElement.Top.Set(0, 0);
            TextElement.TextColor = config.PanelTextColor;

            ChangeAppearance(config.ShowReorderButtons);
        }

        private void MoveUp(UIMouseEvent evt, UIElement listeningElement) {
            AccessibilityModSystem.Displays.Rearrange(this, false);
        }

        private void MoveDown(UIMouseEvent evt, UIElement listeningElement) {
            AccessibilityModSystem.Displays.Rearrange(this, true);
        }

        public void ChangeAppearance(bool showReorderButtons) {
            RemoveAllChildren();

            if(showReorderButtons) {
                TextElement.Left.Set(ChangeOrderButton.Width.Pixels + ElementMargin, 0);
                Append(ChangeOrderButton);
            }
            else {
                TextElement.Left.Set(0, 0);
            }

            Append(TextElement);

            Height.Set(ChangeOrderButton.Height.Pixels, 0);
            Width.Set(ChangeOrderButton.Width.Pixels, 0);
        }

        public void SetFormattedText(string text) {
            TextElement.SetText(string.Format(Format, text));
        }

        public void ResetText() {
            TextElement.SetText(string.Format(Format, Language.GetTextValue(translation)));
        }

        public override void Update(GameTime gameTime) {
            if(!IsVisible) {
                return;
            }

            base.Update(gameTime);

            if(!TextElement.Text.Contains(translation)) {
                float textWidth = TextElement.MinWidth.Pixels;
                float buttonWidth = ChangeOrderButton.Width.Pixels;

                if(MinWidth.Pixels < textWidth + buttonWidth) {
                    MinWidth = new StyleDimension(textWidth + buttonWidth + ElementMargin, 0);
                }

                if(Parent.MinWidth.Pixels < TextElement.MinWidth.Pixels) {
                    AccessibilityModSystem.UI.SetMinWidth(TextElement.MinWidth);
                }
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
