﻿using System.Linq;
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
    }
}
