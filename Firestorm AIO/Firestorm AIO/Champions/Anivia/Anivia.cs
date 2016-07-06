using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firestorm_AIO.Helpers;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;

using static Firestorm_AIO.Champions.Anivia.ObjManager;
using static Firestorm_AIO.Helpers.Helpers;

namespace Firestorm_AIO.Champions.Anivia
{
    public class Anivia : Bases.ChampionBase
    {
        private int BreakRange = 1100;

        public override void Init()
        {
            Q = new Spell(SpellSlot.Q, 1100);
            W = new Spell(SpellSlot.W, 1000);
            E = new Spell(SpellSlot.E, 600);
            R = new Spell(SpellSlot.R, 750);

            Q.SetSkillshot(0.25f, 110f, 850f, false, SkillshotType.SkillshotLine);
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
            E.CreateBool(LaneClearMenu);
            R.CreateBool(LaneClearMenu);

            Q.CreateBool(LastHitMenu);
            E.CreateBool(LastHitMenu);

            Q.CreateBool(MixedMenu);
            E.CreateBool(MixedMenu);
        }

        public override void Active()
        {
            Target = Variables.TargetSelector.GetTarget(Q.Range, DamageType.Magical);

            if (Q.Instance.ToggleState >= 2 && QObject != null && QObject.Position.CountEnemyHeroesInRange(150) >= 1)
            {
                Q.Cast();
            }
        }

        public override void Combo()
        {
            if (GetBoolValue(Q, ComboMenu))
            {
                if (Q.Instance.ToggleState == 1 && QObject == null)
                {
                    Q.SmartCast(Target, HitChance.High);
                }
            }

            if (GetBoolValue(W, ComboMenu))
            {
                //Cast Wall behind the target if Q is near
                if (QObject != null && QObject.Position.IsInRange(Target, 400))
                {
                    var pos = Me.Position.Extend(Target.Position, W.Range);
                    if (pos.Distance(Me.Position) < W.Range)
                    {
                        W.Cast(pos);
                    }
                }
            }

            if (GetBoolValue(E, ComboMenu))
            {
                //Only if snowed
                if (Target.HasBuffUntil("chilled", 850f))
                {
                    E.SmartCast(Target);
                }
                //To kill
                if(Target.CanKillTarget(E, (int)(Me.Distance(Target) / 850f)))
                {
                    E.SmartCast(Target);
                }
            }

            if (GetBoolValue(R, ComboMenu))
            {
                R.SmartCast(Target, HitChance.High);
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
                if (Target.HasBuffUntil("chilled", 850f))
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
        }

        public override void LastHit()
        {
        }

        public override void KillSteal()
        {
        }

        public override void Draw()
        {
        }
    }
}
