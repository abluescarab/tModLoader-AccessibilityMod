using AccessibilityMod.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace AccessibilityMod {
    public class AccessibilityModSystem : ModSystem {
        private UserInterface modInterface;

        public static AccessibilityModUI UI { get; private set; }
        public static InfoDisplays Displays { get; private set; }

        public override void Load() { 
            if(!Main.dedServ) {
                Displays = new InfoDisplays();
                modInterface = new UserInterface();
                UI = new AccessibilityModUI();

                UI.Activate();
                modInterface.SetState(UI);
            }
        }

        public override void Unload() {
            UI = null;
        }

        public override void UpdateUI(GameTime gameTime) {
            if(UI.IsVisible) {
                modInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int inventoryLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

            if(inventoryLayer != -1) {
                layers.Insert(
                    inventoryLayer,
                    new LegacyGameInterfaceLayer(
                        "Accessibility Mod: Custom UI",
                        () => {
                            if(UI.IsVisible) {
                                modInterface.Draw(Main.spriteBatch, new GameTime());
                            }

                            return true;
                        },
                        InterfaceScaleType.UI));
            }
        }
    }
}
