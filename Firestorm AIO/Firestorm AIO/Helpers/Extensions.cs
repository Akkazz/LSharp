﻿using System;
using System.Linq;
using System.Windows.Forms;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;

using static Firestorm_AIO.Helpers.Helpers;
using Menu = LeagueSharp.SDK.UI.Menu;

namespace Firestorm_AIO.Helpers
{
    public static class Extensions
    {
        public static Obj_AI_Base GetNearestTower(this Obj_AI_Base target)
        {
            return GameObjects.EnemyTurrets.OrderBy(t => t.Distance(target)).FirstOrDefault(t => t.Health > 0 && t.IsValid && !t.IsDead);
        }

        #region bools

        public static bool ItemReady(this Items.Item item)
        {
            return item != null && item.IsOwned(Me) && item.IsReady;
        }

        public static bool SummonerReady(this Spell spell)
        {
            return spell != null && spell.IsReady();
        }

        public static bool IsKnockedUp(this Obj_AI_Base target)
        {
            return target.HasBuffOfType(BuffType.Knockup) || target.HasBuffOfType(BuffType.Knockback);
        }

        public static bool IsUnderTower(this Vector3 position)
        {
            return GameObjects.EnemyTurrets.Where(a => a.Health > 0 && !a.IsDead).Any(a => a.Distance(position) <= 1000);
        }

        public static bool IsKillable(this Obj_AI_Base target, Spell spell)
        {
            return spell.GetDamage(target) >= Health.GetPrediction(target, spell.TravelTime(target));
        }

        #endregion bools

        #region Menus

        public static Menu CreateMenu(this Menu menu, string id, string Name, bool root = false, string uniquestring = "")
        {
            var mewmenu = new Menu(id, Name, root, uniquestring);
            return menu.Add(mewmenu);
        }

        public static MenuBool CreateBool(this Menu menu, string id, string Name, bool value = true, string uniquestring = "")
        {
            return menu.Add(new MenuBool(id, Name, value, uniquestring));
        }

        public static MenuSliderButton CreateSliderButton(this Menu menu, string id, string Name, int dValue = 0, int minValue = 0, int maxValue = 100, bool bValue = true, string uniquestring = "")
        {
            return menu.Add(new MenuSliderButton(id, Name, dValue, minValue, maxValue, bValue, uniquestring));
        }

        public static MenuSlider CreateSlider(this Menu menu, string id, string Name, int dValue = 0, int minValue = 0, int maxValue = 100, string uniquestring = "")
        {
            return menu.Add(new MenuSlider(id, Name, dValue, minValue, maxValue, uniquestring));
        }

        public static MenuKeyBind CreateKeyBind(this Menu menu, string id, string Name, Keys key, KeyBindType type, string uniquestring = "")
        {
            return menu.Add(new MenuKeyBind(id, Name, key, type));
        }

        public static MenuSeparator CreateSeparator(this Menu menu, string id, string Name)
        {
            return menu.Add(new MenuSeparator(id, Name));
        }

        public static void CreateBool(this Spell spell, Menu menu, bool defaultValue = true)
        {
            menu.Add(new MenuBool("use" + spell.Slot, "Use " + spell.Slot.ToString().ToUpper(), defaultValue));
        }

        #endregion Menus

        #region Counts

        #region AllyMinions

        public static int CountAllyMinions(this Obj_AI_Base target, int range)
        {
            return GameObjects.AllyMinions.Count(m => m.IsInRange(target, range) && m.IsValid);
        }

        public static int CountAllyMinions(this Obj_AI_Base target, float range)
        {
            return GameObjects.AllyMinions.Count(m => m.IsInRange(target, range) && m.IsValid);
        }

        public static int CountAllyMinions(this Vector2 position, float range)
        {
            return GameObjects.AllyMinions.Count(m => m.IsInRange(position, range) && m.IsValid);
        }

        public static int CountAllyMinions(this Vector3 position, float range)
        {
            return GameObjects.AllyMinions.Count(m => m.IsInRange(position.ToVector2(), range) && m.IsValid);
        }

        #endregion AllyMinions

        #region EnemyMinions

        public static int CountEnemyMinions(this Obj_AI_Base target, int range)
        {
            return GameObjects.EnemyMinions.Count(m => m.IsInRange(Me, range) && m.IsValid);
        }

        public static int CountEnemyMinions(this Obj_AI_Base target, float range)
        {
            return GameObjects.EnemyMinions.Count(m => m.IsInRange(Me, range) && m.IsValid);
        }

        public static int CountEnemyMinions(this Vector2 position, float range)
        {
            return GameObjects.EnemyMinions.Count(m => m.IsInRange(Me, range) && m.IsValid);
        }

        public static int CountEnemyMinions(this Vector3 position, float range)
        {
            return GameObjects.EnemyMinions.Count(m => m.IsInRange(Me, range) && m.IsValid);
        }

        #endregion EnemyMinions

        #region AnyEnemy

        public static int CountAnyEnemy(this Obj_AI_Base target, int range)
        {
            return GameObjects.Enemy.Count(m => m.IsInRange(Me, range) && m.IsValid);
        }

        public static int CountAnyEnemy(this Obj_AI_Base target, float range)
        {
            return GameObjects.Enemy.Count(m => m.IsInRange(Me, range) && m.IsValid);
        }

        public static int CountAnyEnemy(this Vector2 position, float range)
        {
            return GameObjects.Enemy.Count(m => m.IsInRange(Me, range) && m.IsValid);
        }

        public static int CountAnyEnemy(this Vector3 position, float range)
        {
            return GameObjects.Enemy.Count(m => m.IsInRange(Me, range) && m.IsValid);
        }

        #endregion AnyEnemy

        #endregion Counts

        #region Vector

        public static bool IsInRange(this Obj_AI_Base target, Obj_AI_Base from, int range)
        {
            return target.Distance(from) < range;
        }

        public static bool IsInRange(this Obj_AI_Base target, Obj_AI_Base from, float range)
        {
            return target.Distance(from) < range;
        }

        public static bool IsInRange(this Obj_AI_Base target, Vector3 from, int range)
        {
            return target.Distance(from) < range;
        }

        public static bool IsInRange(this Obj_AI_Base target, Vector2 from, float range)
        {
            return target.Distance(from) < range;
        }

        public static bool IsInRange(this Vector3 target, Vector3 from, float range)
        {
            return target.Distance(from) < range;
        }

        public static bool IsInRange(this Vector3 target, Vector2 from, float range)
        {
            return target.Distance(from) < range;
        }

        public static bool IsInRange(this Vector2 target, Vector3 from, float range)
        {
            return target.Distance(from) < range;
        }

        public static bool IsInRange(this Vector2 target, Vector2 from, float range)
        {
            return target.Distance(from) < range;
        }

        public static bool IsInRange(this Vector3 target, Obj_AI_Base from, float range)
        {
            return target.Distance(from.Position) < range;
        }

        public static bool IsInRange(this Vector2 target, Obj_AI_Base from, float range)
        {
            return target.Distance(from.Position) < range;
        }

        public static bool IsInRange(this GameObject target, Obj_AI_Base from, float range)
        {
            return target.Distance(from.Position) < range;
        }

        public static bool IsInRange(this GameObject target, Vector3 from, float range)
        {
            return target.Distance(from) < range;
        }

        public static bool IsInAARange(this Obj_AI_Base target, int plusRange = 0)
        {
            return target.Distance(Me) < Me.GetRealAutoAttackRange() + 0;
        }

        #endregion Vector

        #region Damage

        public static bool CanKillTarget(this Obj_AI_Base target, Spell spell, int customDelay = 0)
        {
            var predictedHealth = Health.GetPrediction(target, customDelay == 0 ? (int)(spell.Delay * 1000f) : customDelay);

            return predictedHealth < spell.GetDamage(target) && predictedHealth >= Me.GetAutoAttackDamage(target);
        }

        public static bool CanKillTarget(this Obj_AI_Base target, float damage, int delay)
        {
            var predictedHealth = Health.GetPrediction(target, (int)(delay * 1000f));

            return predictedHealth < damage && predictedHealth >= Me.GetAutoAttackDamage(target);
        }

        #endregion Damage

        #region Times

        public static int TravelTime(this Spell spell, Obj_AI_Base target)
        {
            return (int)((target.DistanceToPlayer() / spell.Speed) + (Math.Abs(spell.Delay + Game.Ping)));
        }

        public static int TravelTime(this Spell spell, Obj_AI_Base From, Obj_AI_Base To)
        {
            return (int)((From.Distance(To) / spell.Speed) + (Math.Abs(spell.Delay + Game.Ping)));
        }

        public static int TravelTime(this Spell spell, Vector2 From, Vector2 To)
        {
            return (int)((From.Distance(To) / spell.Speed) + (Math.Abs(spell.Delay + Game.Ping)));
        }

        public static int TravelTime(this Spell spell, Vector3 From, Vector3 To)
        {
            return (int)((From.Distance(To) / spell.Speed) + (Math.Abs(spell.Delay + Game.Ping)));
        }

        public static int TravelTime(this Spell spell, Obj_AI_Base From, Vector3 To)
        {
            return (int)((From.Distance(To) / spell.Speed) + (Math.Abs(spell.Delay + Game.Ping)));
        }

        public static int TravelTime(this Spell spell, Vector3 From, Obj_AI_Base To)
        {
            return (int)((From.Distance(To.ServerPosition) / spell.Speed) + (Math.Abs(spell.Delay + Game.Ping)));
        }

        public static int TravelTime(this Spell spell, Obj_AI_Base From, Vector2 To)
        {
            return (int)((From.Distance(To) / spell.Speed) + (Math.Abs(spell.Delay + Game.Ping)));
        }

        public static int TravelTime(this Spell spell, Vector2 From, Obj_AI_Base To)
        {
            return (int)((From.Distance(To.ServerPosition) / spell.Speed) + (Math.Abs(spell.Delay + Game.Ping)));
        }

        #endregion Times
    }
}
