using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace AccessibilityMod.UI {
    internal struct InfoDisplays {
        private static Dictionary<string, InfoDisplay> displays 
            = new Dictionary<string, InfoDisplay>() {
                {
                    OreTooltips,
                    new("Ore: {0}", 
                        () => ModContent.GetInstance<AccessibilityModConfig>()
                        .ShowOreTooltips)
                },
                {
                    BackgroundWallAvailable,
                    new("Wall: {0}", 
                        () => ModContent.GetInstance<AccessibilityModConfig>()
                        .ShowBackgroundWallAvailable)
                }
            };

        public const string OreTooltips = "OreTooltips";
        public const string BackgroundWallAvailable = "BackgroundWallAvailable";

        public static void Add(string name, string format, Func<bool> isVisible) {
            displays.Add(name, new(format, isVisible));
        }

        public static void Remove(string name) {
            displays.Remove(name);
        }

        public static InfoDisplay Get(string name) {
            return displays[name];
        }

        public static InfoDisplay[] GetAll() {
            return displays.Values.ToArray();
        }

        public static void SetText(string name, string text) {
            displays[name].SetFormattedText(text);
        }

        public static InfoDisplay[] GetVisible() {
            return displays.Values.Where(d => d.IsVisible).ToArray();
        }
    }
}
