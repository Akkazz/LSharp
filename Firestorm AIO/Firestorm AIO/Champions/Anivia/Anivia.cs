using Firestorm_AIO.Helpers;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

using static Firestorm_AIO.Champions.Anivia.ObjManager;
using static Firestorm_AIO.Helpers.Helpers;

namespace Firestorm_AIO.Champions.Anivia
{
    public class Anivia : Bases.ChampionBase
    {
        private const int BreakRange = 1100;
        private const int Q2Range = 200;
        private const int R2Range = 400;

        public override void Init()
        {
            Q = new Spell(SpellSlot.Q, 1100);
            W = new Spell(SpellSlot.W, 1000);
            E = new Spell(SpellSlot.E, 600);
            R = new Spell(SpellSlot.R, 750);

            Q.SetSkillshot(0.25f, 200f, 850f, false, SkillshotType.SkillshotLine);
            R.SetSkillshot(0.25f, 200f, float.MaxValue, false, SkillshotType.SkillshotCircle);

            Q.IsAntiGapCloser();

            ObjManager.Load();
        }

        public override void Menu()
        {
            Q.CreateBool(ComboMenu);
            W.CreateBool(ComboMenu);
            E.CreateBool(ComboMenu);
            R.CreateBool(ComboMenu);

            Q.CreateBool(LaneClearMenu);
            LaneClearMenu.Add(new MenuSlider("qCount", "Only if R will hit X", 3, 0, 9));
            E.CreateBool(LaneClearMenu);
            R.CreateBool(LaneClearMenu);
            LaneClearMenu.Add(new MenuSlider("rCount", "Only if R will hit X", 3, 0, 9));

            Q.CreateBool(JungleClearMenu);
            E.CreateBool(JungleClearMenu);

            Q.CreateBool(LastHitMenu);
            LastHitMenu.Add(new MenuSlider("qCount", "Only if R will hit X", 1, 0, 9));
            E.CreateBool(LastHitMenu);

            Q.CreateBool(MixedMenu);
            E.CreateBool(MixedMenu);
        }

        public override void Active()
        {
            Target = Variables.TargetSelector.GetTarget(Q.Range, DamageType.Magical);
        }

        public override void Combo()
        {
            if (GetBoolValue(Q, ComboMenu))
            {
                if (Q.Instance.ToggleState == 1 && QObject == null)
                {
                    Q.SmartCast(Target, HitChance.High);
                }

                if (Q.Instance.ToggleState >= 2 && QObject != null && QObject.Position.IsInRange(Target, Q2Range))
                {
                    Q.Cast();
                }
            }

            if (GetBoolValue(W, ComboMenu))
            {
                //Cast Wall behind the target if Q is near
                if (QObject != null && QObject.Position.IsInRange(Target, 350))
                {
                    var pos = Me.Position.Extend(Target.Position, Me.Distance(Target) + 120);
                    if (pos.Distance(Me.Position) < W.Range)
                    {
                        W.Cast(pos);
                    }
                }
            }

            if (GetBoolValue(E, ComboMenu))
            {
                //Only if snowed
                if (Target.HasBuff("chilled"))
                {
                    E.SmartCast(Target);
                }
                //To kill
                if (Target.CanKillTarget(E, (int)(Me.Distance(Target) / 850f)))
                {
                    E.SmartCast(Target);
                }
            }

            if (GetBoolValue(R, ComboMenu))
            {
                if (R.Instance.ToggleState == 1 && RObject == null && !Target.HasBuff("chilled"))
                {
                    R.SmartCast(Target, HitChance.High);
                }

                if (R.Instance.ToggleState >= 2 && RObject != null && RObject.Position.CountEnemyHeroesInRange(R2Range) <= 0)
                {
                    R.Cast();
                }
            }
        }

        public override void Mixed()
        {
            if (GetBoolValue(Q, MixedMenu))
            {
                if (Q.Instance.ToggleState == 1 && QObject == null)
                {
                    Q.SmartCast(Target, HitChance.High);
                }
            }

            if (GetBoolValue(E, MixedMenu))
            {
                //Only if snowed
                if (Target.HasBuff("chilled"))
                {
                    E.SmartCast(Target);
                }
                //To kill
                if (Target.CanKillTarget(E, (int)(Me.Distance(Target) / 850f)))
                {
                    E.SmartCast(Target);
                }
            }
        }

        public override void LaneClear()
        {
            if (GetBoolValue(Q, LaneClearMenu))
            {
                if (Q.Instance.ToggleState == 1 && QObject == null)
                {
                    Q.SmartCast(null, HitChance.Medium, LaneClearMenu["qCount"]);
                }

                if (Q.Instance.ToggleState >= 2 && QObject != null && QObject.Position.CountEnemyMinions(Q2Range) >= LaneClearMenu["qCount"])
                {
                    Q.Cast();
                }
            }

            if (GetBoolValue(R, LaneClearMenu))
            {
                if (GetBoolValue(R, LaneClearMenu) && R.Instance.ToggleState == 1 && RObject == null)
                {
                    R.SmartCast(R.GetBestLaneClearMinion(), HitChance.High);
                }
            }

            //JungleClear

            if (GetBoolValue(Q, JungleClearMenu))
            {
                var minionQ = Q.GetBestJungleClearMinion();

                if (Q.Instance.ToggleState == 1 && QObject == null)
                {
                    Q.SmartCast(minionQ);
                }

                if (Q.Instance.ToggleState >= 2 && QObject != null && QObject.Position.IsInRange(minionQ, Q2Range))
                {
                    Q.Cast();
                }
            }

            if (GetBoolValue(E, JungleClearMenu))
            {
                var minionE = E.GetBestJungleClearMinion();

                //Only if snowed
                if (minionE.HasBuff("chilled"))
                {
                    E.SmartCast(minionE);
                }
                //To kill
                if (minionE.CanKillTarget(E, (int)(Me.Distance(Target) / 850f)))
                {
                    E.SmartCast(minionE);
                }
            }
        }

        public override void LastHit()
        {
            if (GetBoolValue(Q, LastHitMenu))
            {
                Q.SmartCast(Q.GetBestLastHitMinion(), HitChance.Medium, LastHitMenu["qCount"]);
            }

            if (GetBoolValue(E, LastHitMenu))
            {
                E.SmartCast(E.GetBestLastHitMinion());
            }
        }

        public override void KillSteal()
        {
        }

        public override void Draw()
        {
            if (QObject != null)
            {
                Render.Circle.DrawCircle(QObject.Position, Q2Range, QColor);
            }

            if (RObject != null)
            {
                Render.Circle.DrawCircle(RObject.Position, BreakRange, RColor);
            }
        }
    }
}
