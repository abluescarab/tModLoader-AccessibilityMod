using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace AccessibilityMod.UI {
    public class UIUpDownButton : UIImage {
		public event MouseEvent OnClickUpButton;
        public event MouseEvent OnClickDownButton;

        public UIUpDownButton() : base(UICommon.ButtonUpDownTexture) {
            Width.Set(UICommon.ButtonUpDownTexture.Value.Width, 0);
            Height.Set(UICommon.ButtonUpDownTexture.Value.Height, 0);

            OnClick += (a, b) => {
                Rectangle r = b.GetDimensions().ToRectangle();

                if(a.MousePosition.Y < r.Y + r.Height / 2) {
                    OnClickUpButton?.Invoke(new UIMouseEvent(this, Main.MouseScreen), this);
                }
                else {
                    OnClickDownButton?.Invoke(new UIMouseEvent(this, Main.MouseScreen), this);
                }

                if(Parent is AccessibilityModUI) {
                    ((AccessibilityModUI)Parent).CreateChildren();
                }
            };
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);

            Rectangle r = GetDimensions().ToRectangle();

            if(IsMouseHovering) {
                if(Main.mouseY < r.Y + r.Height / 2) {
                    Main.instance.MouseText(
                        Language.GetTextValue("Mods.AccessibilityMod.MoveUp_Label"));
                }
                else {
                    Main.instance.MouseText(
                        Language.GetTextValue("Mods.AccessibilityMod.MoveDown_Label"));
                }

                Main.mouseText = true;
            }
        }
    }
}
