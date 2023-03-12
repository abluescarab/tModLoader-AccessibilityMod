using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace AccessibilityMod.UI {
    public class AccessibilityInfoDisplays {
        private static int nextOrder = 0;

        private Dictionary<string, AccessibilityInfo> displays = new Dictionary<string, AccessibilityInfo>();

        public struct Defaults {
            public const string OreTooltips = "OreTooltips";
            public const string BackgroundWallAvailable = "BackgroundWallAvailable";
        }

        public int Count => displays.Count;

        public AccessibilityInfoDisplays() {
            Add(Defaults.OreTooltips, "Ore: {0}", 
                () => ModContent.GetInstance<AccessibilityModConfig>().ShowOreTooltips);
            Add(Defaults.BackgroundWallAvailable, "Wall: {0}",
                () => ModContent.GetInstance<AccessibilityModConfig>().ShowBackgroundWallAvailable);
        }

        public void Add(string name, string format, Func<bool> isVisible) {
            displays.Add(name, new(format, isVisible, nextOrder++));
        }

        public void Remove(string name) {
            displays.Remove(name);
        }

        public AccessibilityInfo Get(string name) {
            return displays[name];
        }

        public AccessibilityInfo[] GetAll(bool sorted = true) {
            if(sorted) {
                List<AccessibilityInfo> list = displays.Values.ToList();
                list.Sort((d1, d2) => d1.Order.CompareTo(d2.Order));

                return list.ToArray();
            }
            else {
                return displays.Values.ToArray();
            }
        }

        public void SetText(string name, string text) {
            displays[name].SetFormattedText(text);
        }

        public AccessibilityInfo[] GetVisible(bool sorted = true) {
            return GetAll(sorted).Where(d => d.IsVisible).ToArray();
        }

        internal void Rearrange(AccessibilityInfo display, bool increment) {
            int order = display.Order;
            int index = Array.FindIndex(GetAll(true), d => d == display);
            AccessibilityInfo nextDisplay;

            if(increment && order < Count - 1) {
                nextDisplay = GetAll().FirstOrDefault(d => d.Order == order + 1);
                nextDisplay.Order--;
                display.Order++;
            }
            else if(!increment && order > 0) {
                nextDisplay = GetAll().FirstOrDefault(d => d.Order == order - 1);
                nextDisplay.Order++;
                display.Order--;
            }

            AccessibilityModSystem.UI.CreateChildren();
        }
    }
}
