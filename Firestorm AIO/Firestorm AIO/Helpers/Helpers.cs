using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;

namespace Firestorm_AIO.Helpers
{
    public static class Helpers
    {
        public static Obj_AI_Hero Me => GameObjects.Player;

        public static Obj_AI_Minion GetBestJungleClearMinion(this Spell spell)
        {
            return GameObjects.Jungle.Where(spell.CanCast).OrderByDescending(m => m.MaxHealth).FirstOrDefault();
        }

        public static Obj_AI_Minion GetBestLastHitMinion(this Spell spell)
        {
            return
                GameObjects.EnemyMinions.Where(m => m.IsValidTarget(spell.Range))
                    .OrderBy(m => m.Health)
                    .FirstOrDefault(m => Health.GetPrediction(m, (int) spell.Delay*1000) < spell.GetDamage(m));
        }

        public static Obj_AI_Minion GetBestLaneClearMinion(this Spell spell)
        {
            if (spell.IsSkillshot)
            {
                return
                    GameObjects.EnemyMinions.Where(m => m.IsValidTarget(spell.Range))
                        .OrderBy(m => m.CountEnemyMinions(350))
                        .ThenBy(m => m.Health)
                        .FirstOrDefault(m => m.CanKillTarget(spell));
            }
            return
                GameObjects.EnemyMinions.Where(m => m.IsValidTarget(spell.Range))
                    .OrderBy(m => m.Health)
                    .FirstOrDefault(m => m.CanKillTarget(spell));
        }

        #region Lists

        public static Dictionary<BuffType, bool> DeBuffs = new Dictionary<BuffType, bool>()
                                                               {
                                                                   { BuffType.AttackSpeedSlow, false }, { BuffType.Blind, true }, { BuffType.Charm, true }, { BuffType.Disarm, false },
                                                                   { BuffType.Fear, true }, { BuffType.Knockback, true }, { BuffType.Knockup, true }, { BuffType.NearSight, false },
                                                                   { BuffType.Poison, false }, { BuffType.Polymorph, false }, { BuffType.Silence, true }, { BuffType.Slow, false },
                                                                   { BuffType.Snare, true }, { BuffType.Stun, true }, { BuffType.Suppression, true }, { BuffType.Taunt, true }
                                                               };

        public static string[] JungleMobs;

        #endregion Lists
    }
}
