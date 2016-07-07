using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using static Firestorm_AIO.Helpers.Helpers;

namespace Firestorm_AIO.Helpers
{
    public static class SmartCaster
    {
        #region GetTarget

        public static Obj_AI_Minion GetBestLastHitMinion(this Spell spell)
        {
            return
                GameObjects.EnemyMinions.Where(m => m.IsValidTarget(spell.Range))
                    .OrderBy(m => m.Health)
                    .FirstOrDefault(m => Health.GetPrediction(m, (int) spell.Delay*1000) < spell.GetDamage(m));
        }

        #endregion GetTarget

        public static void SmartCast(this Spell spell, Obj_AI_Base target = null, HitChance hitchance = HitChance.Medium, int minimunHits = 0)
        {
            var orbMode = Variables.Orbwalker.GetActiveMode();

            var hero = target as Obj_AI_Hero;
            if (hero != null && (orbMode.HasFlag(OrbwalkingMode.Combo) || orbMode.HasFlag(OrbwalkingMode.Hybrid)))
            {
                if (!spell.CanCast(target)) return;

                if (spell.IsSkillshot)
                {
                    if (spell.Collision)
                    {
                        if (Me.CountEnemyHeroesInRange(spell.Range) <= (minimunHits == 0 ? 2 : minimunHits))
                        {
                            spell.CastOnBestTarget(0f, true);
                        }

                        if (Me.CountEnemyHeroesInRange(spell.Range) >= (minimunHits == 0 ? 2 : minimunHits))
                        {
                            spell.CastOnBestTarget(0f, true, 1);
                        }
                    }
                    else
                    {
                        spell.CastIfHitchanceMinimum(target, hitchance);
                    }
                }
                else
                {
                    spell.CastOnUnit(target);
                }
            }

            var minion = target as Obj_AI_Minion;
            if (minion != null)
            {
                if (spell.IsSkillshot)
                {
                    if (spell.Collision)
                    {
                        spell.CastIfHitchanceMinimum(target, HitChance.Low);
                    }
                    else
                    {
                        if (orbMode.HasFlag(OrbwalkingMode.LaneClear))
                        {
                            var minions =
                                GameObjects.EnemyMinions.Where(m => m.IsValidTarget())
                                    .OrderBy(m => m.Distance(target))
                                    .ThenBy(m => m.Health)
                                    .ToList();

                            if (minions != null)
                            {
                                switch (spell.Type)
                                {
                                    case SkillshotType.SkillshotLine:
                                        var posLine = spell.GetLineFarmLocation(minions);
                                        if (posLine.MinionsHit >= (minimunHits == 0 ? 1 : minimunHits))
                                        {
                                            spell.Cast(posLine.Position);
                                        }
                                        break;
                                    case SkillshotType.SkillshotCircle:
                                        var posCircle = spell.GetCircularFarmLocation(minions);
                                        if (posCircle.MinionsHit >= (minimunHits == 0 ? 1 : minimunHits))
                                        {
                                            spell.Cast(posCircle.Position);
                                        }
                                        break;
                                    case SkillshotType.SkillshotCone:
                                        var posCone = spell.GetLineFarmLocation(minions, spell.Width);
                                        if (posCone.MinionsHit >= (minimunHits == 0 ? 1 : minimunHits))
                                        {
                                            spell.Cast(posCone.Position);
                                        }
                                        break;
                                }
                            }
                            spell.Cast(target.Position);
                        }

                        if (orbMode.HasFlag(OrbwalkingMode.LastHit))
                        {
                            spell.CastIfHitchanceMinimum(target, HitChance.Low);
                        }
                    }
                }
                else
                {
                    spell.CastOnUnit(target);
                }
            }
        }
    }
}
