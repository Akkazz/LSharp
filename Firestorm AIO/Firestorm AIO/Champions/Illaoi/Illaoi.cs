using System.Linq;
using Firestorm_AIO.Helpers;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using static Firestorm_AIO.Helpers.Helpers;

namespace Firestorm_AIO.Champions.Illaoi
{
    public class Illaoi : Bases.ChampionBase
    {
        public override void Init()
        {
            Q = new Spell(SpellSlot.Q, 850);
            W = new Spell(SpellSlot.W, 350);
            E = new Spell(SpellSlot.E, 950);
            R = new Spell(SpellSlot.R, 500);

            Q.SetSkillshot(0.75f, 100f, float.MaxValue, false, SkillshotType.SkillshotLine);
            E.SetSkillshot(0.25f, 50f, 1900f, true, SkillshotType.SkillshotLine);

            Variables.Orbwalker.OnAction += Orbwalker_OnAction;
        }

        private void Orbwalker_OnAction(object sender, OrbwalkingActionArgs e)
        {
            //AA Cancel
            if (Target != null && Target.IsValidTarget(Me.GetRealAutoAttackRange()) && e.Type == OrbwalkingType.AfterAttack)
            {
                W.Cast();
            }
        }

        public override void Menu()
        {
            Q.CreateBool(ComboMenu);
            W.CreateBool(ComboMenu);
            E.CreateBool(ComboMenu);
            R.CreateBool(ComboMenu);
            ComboMenu.Add(new MenuSlider("rCount", "Only R if there are X in range", 2, 0, 5));

            Q.CreateBool(MixedMenu);

            Q.CreateBool(LaneClearMenu);

            Q.CreateBool(LastHitMenu);

            Q.CreateBool(JungleClearMenu);
        }

        public override void Active()
        {
            Target = Variables.TargetSelector.GetTarget(950, DamageType.Physical);
        }

        public override void Combo()
        {
            if (GetBoolValue(Q, ComboMenu))
            {
                Q.SmartCast(Target);
            }

            if (GetBoolValue(E, ComboMenu))
            {
                E.SmartCast(Target, HitChance.High);
            }

            if (GetBoolValue(W, ComboMenu))
            {
                if (!Target.IsInAARange())
                {
                    W.Cast();
                }
            }

            var countR =
                GameObjects.EnemyHeroes.Count(e => Movement.GetPrediction(e, 500f).UnitPosition.IsInRange(Me, R.Range));
            if (countR >= ComboMenu["rCount"] && Me.HealthPercent >= 15)
            {
                R.Cast();
            }
             
        }

        public override void Mixed()
        {
            if (GetBoolValue(Q, MixedMenu))
            {
                Q.SmartCast(Target);
            }
        }

        public override void LaneClear()
        {
            if (GetBoolValue(Q, LaneClearMenu))
            {
                Q.SmartCast(Target);
            }

            //JungleClear

            if (GetBoolValue(Q, JungleClearMenu))
            {
                Q.SmartCast(Q.GetBestJungleClearMinion());
            }
        }

        public override void LastHit()
        {
            if (GetBoolValue(Q, LastHitMenu))
            {
                Q.SmartCast(Q.GetBestLastHitMinion());
            }
        }

        public override void KillSteal()
        {
            if (Target.CanKillTarget(Q))
            {
                Q.SmartCast(Target);
            }
        }

        public override void Draw()
        {
        }
    }
}
