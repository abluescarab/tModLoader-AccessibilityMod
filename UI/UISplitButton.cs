using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace AccessibilityMod.UI {
    // Modified from tModLoader UIModConfigHoverImageSplit
    public class UISplitButton : UIImage {
        /// <summary>
        /// Fires when the up button is clicked.
        /// </summary>
        public event MouseEvent OnClickUpButton;
        /// <summary>
        /// Fires when the down button is clicked.
        /// </summary>
        public event MouseEvent OnClickDownButton;

        /// <summary>
        /// Text to display when the up button is hovered over.
        /// </summary>
        public string HoverTextUp { get; set; }
        /// <summary>
        /// Text to display when the down button is hovered over.
        /// </summary>
        public string HoverTextDown { get; set; }

        public UISplitButton(string hoverTextUp, string hoverTextDown) 
            : base(UICommon.ButtonUpDownTexture) {
            HoverTextUp = hoverTextUp;
            HoverTextDown = hoverTextDown;

            Width.Set(UICommon.ButtonUpDownTexture.Value.Width, 0);
            Height.Set(UICommon.ButtonUpDownTexture.Value.Height, 0);

            OnLeftClick += (evt, element) => {
                Rectangle r = element.GetDimensions().ToRectangle();

                if(evt.MousePosition.Y < r.Y + r.Height / 2) {
                    OnClickUpButton?.Invoke(new UIMouseEvent(this, Main.MouseScreen), this);
                }
                else {
                    OnClickDownButton?.Invoke(new UIMouseEvent(this, Main.MouseScreen), this);
                }
            };
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);

            Rectangle r = GetDimensions().ToRectangle();

            if(IsMouseHovering) {
                if(Main.mouseY < r.Y + r.Height / 2) {
                    Main.instance.MouseText(HoverTextUp);
                }
                else {
                    Main.instance.MouseText(HoverTextDown);
                }

                Main.mouseText = true;
            }
        }
    }
}
