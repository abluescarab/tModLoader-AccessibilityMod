using CustomSlot.UI;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AccessibilityMod.UI {
    public class AccessibilityModUI : UIState {
        public DraggableUIPanel Panel { get; private set; }
        public bool IsVisible { get; set; } = true;
        public static Vector2 DefaultCoordinates => new Vector2(0, 20);

        public override void OnInitialize() {
            Panel = new DraggableUIPanel();

            Panel.HAlign = 0.5f;
            CreateChildren();

            Append(Panel);
        }

        public void SetMinWidth(StyleDimension minWidth) {
            Panel.MinWidth = new StyleDimension(
                minWidth.Pixels + Panel.PaddingLeft * 2 + Panel.PaddingRight * 2,
                minWidth.Precent);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            foreach(AccessibilityDisplay display in AccessibilityModSystem.Displays.GetAll()) {
                display.ResetText();
            }
        }

        public void ResetPosition() {
            Panel.Left.Set(DefaultCoordinates.X, 0);
            Panel.Top.Set(DefaultCoordinates.Y, 0);
        }

        public void SetPosition(float x, float y) {
            Panel.Left.Set(x, 0);
            Panel.Top.Set(y, 0);
        }

        public void CreateChildren() {
            AccessibilityDisplay[] visibleDisplays = AccessibilityModSystem.Displays.GetVisible(true);
            int displayed = visibleDisplays.Length;
            int lineSpacing = FontAssets.MouseText.Value.LineSpacing;
            int index = 0;

            Panel.RemoveAllChildren();

            Panel.MinWidth.Set(0, 0);
            Panel.Height.Set(
                lineSpacing
                * displayed
                + (lineSpacing / 2), 0);

            foreach(AccessibilityDisplay display in visibleDisplays) {
                display.Left.Set(0, 0);
                display.Top.Set(lineSpacing * index++, 0);
                Panel.Append(display);
            }

            if(displayed > 0) {
                IsVisible = true;
            }
            else {
                IsVisible = false;
            }
        }

        public void ShowOreTooltip(int type, bool longRange) {
            AccessibilityModConfig config = ModContent.GetInstance<AccessibilityModConfig>();

            if(!config.ShowOreTooltips ||
                (longRange && !config.EnableLongRangeTooltips)) {
                return;
            }

            AccessibilityModSystem.Displays.SetText(AccessibilityDisplays.Defaults.OreTooltips,
                TileID.Search.GetName(type));
        }

        public void ShowBackgroundWallAvailable() {
            AccessibilityModConfig config = ModContent.GetInstance<AccessibilityModConfig>();

            if(!config.ShowBackgroundWallAvailable) {
                return;
            }

            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];

            AccessibilityModSystem.Displays.SetText(AccessibilityDisplays.Defaults.BackgroundWallAvailable,
                tile.WallType > 0
                ? Language.GetTextValue("Mods.AccessibilityMod.Info.Yes")
                : Language.GetTextValue("Mods.AccessibilityMod.Info.No"));
        }

        public void ShowCanGrappleTo() {
            AccessibilityModConfig config = ModContent.GetInstance<AccessibilityModConfig>();

            if(!config.ShowCanGrappleTo) {
                return;
            }

            bool canGrapple = true;

            Item item = Main.CurrentPlayer.QuickGrapple_GetItemToUse();
            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];

            if(item == null || !tile.HasTile) {
                canGrapple = false;
            }
            else {
                // code modified from game files
                int shoot = item.shoot;

                if(!CanTileBeGrappled(item.shoot, tile)) {
                    canGrapple = false;
                }
                else {
                    Vector2 center = Main.CurrentPlayer.MountedCenter;
                    Vector2 mouse = Main.MouseWorld;
                    float distX = center.X - mouse.X;
                    float distY = center.Y - mouse.Y;
                    // -16 to add an extra block for safety
                    float distance = (float)Math.Sqrt(distX * distX + distY * distY) - 16f;

                    if((distance > 300f && shoot == ProjectileID.Hook)
                        || (distance > 400f && shoot == ProjectileID.IvyWhip)
                        || (distance > 440f && shoot == ProjectileID.DualHookBlue)
                        || (distance > 440f && shoot == ProjectileID.DualHookRed)
                        || (distance > 375f && shoot == ProjectileID.Web)
                        || (distance > 350f && shoot == ProjectileID.SkeletronHand)
                        || (distance > 500f && shoot == ProjectileID.BatHook)
                        || (distance > 550f && shoot == ProjectileID.WoodHook)
                        || (distance > 400f && shoot == ProjectileID.CandyCaneHook)
                        || (distance > 550f && shoot == ProjectileID.ChristmasHook)
                        || (distance > 400f && shoot == ProjectileID.FishHook)
                        || (distance > 300f && shoot == ProjectileID.SlimeHook)
                        || (distance > 550f && shoot >= ProjectileID.LunarHookSolar && shoot <= ProjectileID.LunarHookStardust)
                        || (distance > 600f && shoot == ProjectileID.StaticHook)
                        || (distance > 300f && shoot == ProjectileID.SquirrelHook)
                        || (distance > 500f && shoot == ProjectileID.QueenSlimeHook)
                        || (distance > 480f && shoot >= ProjectileID.TendonHook && shoot <= ProjectileID.WormHook)
                        || (distance > 500f && shoot == ProjectileID.AntiGravityHook)) {
                        canGrapple = false;
                    }
                    else if(shoot >= ProjectileID.GemHookAmethyst && shoot <= ProjectileID.GemHookDiamond) {
                        int modifier = 300 + (shoot - ProjectileID.GemHookAmethyst) * 30;

                        if(distance > modifier) {
                            canGrapple = false;
                        }
                    }
                    else if(shoot == ProjectileID.AmberHook) {
                        int modifier = 420;

                        if(distance > modifier) {
                            canGrapple = false;
                        }
                    }
                }
            }

            AccessibilityModSystem.Displays.SetText(AccessibilityDisplays.Defaults.CanGrappleTo,
                canGrapple
                ? Language.GetTextValue("Mods.AccessibilityMod.Info.Yes")
                : Language.GetTextValue("Mods.AccessibilityMod.Info.No"));
        }

        private bool CanTileBeGrappled(int grapple, Tile tile) {
            return Main.tileSolid[tile.TileType]
                | tile.TileType == 314
                | (grapple == 865 && TileID.Sets.IsATreeTrunk[tile.TileType])
                | (grapple == 865 && tile.TileType == 323);
        }
    }
}
