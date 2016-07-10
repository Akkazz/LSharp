using System;
using System.Windows.Forms;
using Firestorm_AIO.DataBases;
using Firestorm_AIO.Enums;
using Firestorm_AIO.Helpers;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

using Color = SharpDX.Color;
using static Firestorm_AIO.Helpers.Helpers;
using Menu = LeagueSharp.SDK.UI.Menu;


namespace Firestorm_AIO.Bases
{
    public abstract class ChampionBase
    {
        public static Obj_AI_Hero Target;
        public static Champion MyChampion = Me.GetChampion();

        public static bool HasMana = true;
        public static bool FleeKey;

        public bool CanFlee;

        #region Spells

        public static Spell Q;
        public static Spell W;
        public static Spell E;
        public static Spell R;

        #endregion Spells

        #region Modes

        public abstract void Init();
        public abstract void Menu();
        public abstract void Active();
        public abstract void Combo();
        public abstract void Mixed();
        public abstract void LaneClear();
        public abstract void LastHit();
        public abstract void KillSteal();
        //
        public abstract void Draw();

        #endregion Modes

        #region Menus

        public Menu ChampionMenu;
        public Menu ComboMenu;
        public Menu MixedMenu;
        public Menu LaneClearMenu;
        public Menu JungleClearMenu;
        public Menu LastHitMenu;
        public Menu KillstealMenu;
        public Menu DrawingMenu;
        public Menu MiscMenu;

        //Colors
        private bool DrawReady;

        private bool DrawQ;
        public System.Drawing.Color QColor;

        private bool DrawW;
        public System.Drawing.Color WColor;

        private bool DrawE;
        public System.Drawing.Color EColor;

        private bool DrawR;
        public System.Drawing.Color RColor;

        public void DrawSpell(Spell spell)
        {
            if (spell == null) return;

            var color = System.Drawing.Color.White;
            var CanDraw = true;

            switch (spell.Slot)
            {
                case SpellSlot.Q:
                    color = QColor;
                    CanDraw = DrawQ;
                    break;
                case SpellSlot.W:
                    color = WColor;
                    CanDraw = DrawW;
                    break;
                case SpellSlot.E:
                    color = EColor;
                    CanDraw = DrawE;
                    break;
                case SpellSlot.R:
                    color = RColor;
                    CanDraw = DrawR;
                    break;
            }

            if (CanDraw && (!DrawReady || spell.IsReady())) Render.Circle.DrawCircle(Me.Position, spell.Range, color);
        }

        #endregion Menus

        #region Functions
        public bool GetBoolValue(Spell spell, Menu menu)
        {
            return ChampionMenu[menu.Name]["use" + spell.Slot];
        }

        public bool GetBoolValue(string name, Menu menu)
        {
            return ChampionMenu[menu.Name][name];
        }

        #endregion Functions

        public void Load()
        {
            Init();

            #region InitialMenu

            ChampionMenu = Loader.MainMenu.Add(new Menu(Me.ChampionName + "Menu", Me.ChampionName + " Menu"));
            ChampionMenu.CreateSeparator("a", Me.ChampionName);
            ComboMenu = ChampionMenu.Add(new Menu("comboMenu" + Me.ChampionName, "Combo Menu"));
            MixedMenu = ChampionMenu.Add(new Menu("mixedMenu" + Me.ChampionName, "Mixed Menu"));
            LaneClearMenu = ChampionMenu.Add(new Menu("laneMenu" + Me.ChampionName, "LaneClear Menu"));
            JungleClearMenu = ChampionMenu.Add(new Menu("jungleMenu" + Me.ChampionName, "JungleClear Menu"));
            LastHitMenu = ChampionMenu.Add(new Menu("lastMenu" + Me.ChampionName, "LastHit Menu"));
            KillstealMenu = ChampionMenu.Add(new Menu("ksMenu" + Me.ChampionName, "KillSteal Menu"));
            DrawingMenu = ChampionMenu.Add(new Menu("drawMenu" + Me.ChampionName, "Drawing Menu"));

            var drawReady = DrawingMenu.Add(new MenuBool("drawReady", "Only draw spell if it is ready"));
            DrawReady = drawReady.Value;
            drawReady.ValueChanged += delegate
            {
                DrawReady = drawReady.Value;
            };
            DrawingMenu.Add(new MenuSeparator("separator1", "   "));

            MiscMenu = ChampionMenu.Add(new Menu("miscMenu" + Me.ChampionName, "Misc Menu"));

            if (HasMana)
            {
                MixedMenu.Add(new MenuSliderButton("mixedMana", "Mana percent must be >= x%", 30, 0, 100, true));
                LaneClearMenu.Add(new MenuSliderButton("laneClearMana", "Mana percent must be >= x%", 50, 0, 100, true));
                JungleClearMenu.Add(new MenuSliderButton("jungleClearMana", "Mana percent must be >= x%", 20, 0, 100,
                    true));
                LastHitMenu.Add(new MenuSliderButton("lastHitMana", "Mana percent must be >= x%", 40, 0, 100, true));
            }

            if (Q != null)
            {
                var qDraw = DrawingMenu.Add(new MenuBool("qDraw", "Draw Q ?", true));
                DrawQ = qDraw.Value;
                qDraw.ValueChanged += delegate { DrawQ = qDraw.Value; };

                var qMenuColor = DrawingMenu.Add(new MenuColor("qColor", "Q Color", Color.Black));
                QColor = qMenuColor.Color.ToSystemColor();
                qMenuColor.ValueChanged += delegate { QColor = qMenuColor.Color.ToSystemColor(); };
            }

            if (W != null)
            {
                var wDraw = DrawingMenu.Add(new MenuBool("wDraw", "Draw W ?", true));
                DrawW = wDraw.Value;
                wDraw.ValueChanged += delegate { DrawW = wDraw.Value; };

                var wMenuColor = DrawingMenu.Add(new MenuColor("wColor", "W Color", Color.Blue));
                WColor = wMenuColor.Color.ToSystemColor();
                wMenuColor.ValueChanged += delegate { WColor = wMenuColor.Color.ToSystemColor(); };
            }

            if (E != null)
            {
                var eDraw = DrawingMenu.Add(new MenuBool("eDraw", "Draw E ?", true));
                DrawE = eDraw.Value;
                eDraw.ValueChanged += delegate { DrawE = eDraw.Value; };

                var eMenuColor = DrawingMenu.Add(new MenuColor("eColor", "E Color", Color.DarkKhaki));
                EColor = eMenuColor.Color.ToSystemColor();
                eMenuColor.ValueChanged += delegate { EColor = eMenuColor.Color.ToSystemColor(); };
            }

            if (R != null)
            {
                var rDraw = DrawingMenu.Add(new MenuBool("rDraw", "Draw R ?", true));
                DrawR = rDraw.Value;
                rDraw.ValueChanged += delegate { DrawR = rDraw.Value; };

                var rMenuColor = DrawingMenu.Add(new MenuColor("rColor", "R Color", Color.ForestGreen));
                RColor = rMenuColor.Color.ToSystemColor();
                rMenuColor.ValueChanged += delegate { RColor = rMenuColor.Color.ToSystemColor(); };
            }

            if (FleeKey)
            {
                var flee = MiscMenu.Add(new MenuKeyBind("fleeKey" + Me.ChampionName, "Flee Key", Keys.A, KeyBindType.Press));
                CanFlee = flee.Active;
                flee.ValueChanged += delegate
                {
                    CanFlee = flee.Active;
                };
            }

            #endregion InitialMenu

            Menu();

            Game.OnUpdate += Game_OnUpdate;

            Drawing.OnDraw += Drawing_OnDraw;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            Active();

            if (Me.IsDead) return;

            KillSteal();

            if (!Variables.Orbwalker.Enabled) return;

            var orbMode = Variables.Orbwalker.GetActiveMode();

            if (orbMode.HasFlag(OrbwalkingMode.LaneClear))
            {
                LaneClear();
            }

            if (orbMode.HasFlag(OrbwalkingMode.LastHit))
            {
                LastHit();
            }

            if (Target == null) return;

            if (orbMode.HasFlag(OrbwalkingMode.Combo))
            {
                Combo();
            }

            if (orbMode.HasFlag(OrbwalkingMode.Hybrid))
            {
                Mixed();
            }

            if(!FleeKey)return;


        }

        private void Drawing_OnDraw(EventArgs args)
        {
            if (Me.IsDead) return;

            Draw();

            DrawSpell(Q);
            DrawSpell(W);
            DrawSpell(E);
            DrawSpell(R);
        }
    }
}
