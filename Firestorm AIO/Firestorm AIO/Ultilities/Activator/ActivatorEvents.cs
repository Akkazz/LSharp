using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firestorm_AIO.Helpers;
using LeagueSharp;
using LeagueSharp.SDK;
using static Firestorm_AIO.Helpers.Helpers;

namespace Firestorm_AIO.Ultilities.Activator
{
    public static class ActivatorEvents
    {
        #region EventsStuff

        public delegate void OnDanderEventHandler(Obj_AI_Base sender, EventArgs args);

        public static event OnDanderEventHandler OnDanger;

        #endregion EventsStuff

        public static void Load()
        {
            Game.OnUpdate += Game_OnUpdate;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (OnDanger == null || Me.IsDead) return;

            foreach (var eHero in GameObjects.EnemyHeroes)
            {
                /*
                if (eHero.IsMissileNear())
                {
                    
                }
                */
            }
        }
        /*
        #region Extensions
        
        private struct MyStruct
        {
            
        }

        private static bool IsMissileNear(this Obj_AI_Base target)
        {
            //Missiles
            var missile = Missiles.FirstOrDefault(m => m.IsInRange(target, 3000) && m.IsValid);

            var sliderPercent = 50;
            var boundingRadius = target.BoundingRadius + 80;

            var champion = missile?.SpellCaster as Obj_AI_Hero;

            if (champion != null)
            {
                var spell1 =
                    missile.SpellCaster.Spellbook.Spells.FirstOrDefault(
                        s => s.Name.ToLower().Equals(missile.SData.Name.ToLower()));

                var spell2 =
                    missile.SpellCaster.Spellbook.Spells.FirstOrDefault(
                        s => (s.Name.ToLower() + "missile").Equals(missile.SData.Name.ToLower()));

                var slot = SpellSlot.Unknown;
                if (spell1 != null)
                {
                    slot = spell1.Slot;
                }
                else if (spell2 != null)
                {
                    slot = spell2.Slot;
                }

                var projection = target.Position
                    .ProjectOn(missile.StartPosition, missile.EndPosition);

                if (projection.IsOnSegment &&
                    projection.SegmentPoint.Distance(target.Position) <= missile.SData.CastRadius + boundingRadius)
                {
                    var DangSpell =
                        DangerousSpells.Spells.FirstOrDefault(
                            ds =>
                                ds.Slot == slot && champion.Hero == ds.Hero &&
                                missile.Distance(target) <= boundingRadius + 250 &&
                                Initializer.SettingsMenu.GetCheckBoxValue("dangSpell" + ds.Hero.ToString() +
                                                                          ds.Slot.ToString()));

                    if (DangSpell != null && target.HealthPercent <= percent + sliderPercent)
                    {
                        return true;
                    }
                    return missile.Distance(target) <= boundingRadius && target.HealthPercent <= percent;
                }

                return missile.Distance(target) <= boundingRadius && target.HealthPercent <= percent;
            }
            return false;
        }

        #endregion Extensions
        */
        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender == null || OnDanger == null || Me.IsDead) return;
        }

        #region Missiles Stuff

        private static List<MissileClient> Missiles = new List<MissileClient>();

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var missile = sender as MissileClient;
            if (missile == null || OnDanger == null || Me.IsDead || sender.IsAlly) return;

            var senderHero = missile.SpellCaster as Obj_AI_Hero;
            if (senderHero == null) return;

            Missiles.Add(missile);
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            var missile = sender as MissileClient;
            if (missile == null || OnDanger == null || sender.IsAlly) return;

            var senderHero = missile.SpellCaster as Obj_AI_Hero;
            if (senderHero == null) return;

            Missiles.Remove(missile);
        }

        #endregion Missiles Stuff


    }

    public class OnDangerEventArgs : EventArgs
    {
        public string Status { get; private set; }
        public string DangerLevel { get; private set; }

        public OnDangerEventArgs(string status)
        {
            Status = status;
        }
    }
}
