using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firestorm_AIO.Bases;
using Firestorm_AIO.DataBases;
using Firestorm_AIO.Enums;
using Firestorm_AIO.Helpers;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;
using static Firestorm_AIO.Helpers.Helpers;

namespace Firestorm_AIO.Ultilities.Activator
{
    public static class ActivatorEvents
    {
        #region EventsStuff

        public class OnDangerEventArgs : EventArgs
        {
            public int DangerLevel { get; private set; }
            public SpellSlot Slot { get; private set; }
            public Champion Champion { get; private set; }

            public OnDangerEventArgs(int dangerLevel, SpellSlot slot, Champion champ)
            {
                DangerLevel = dangerLevel;
                Slot = slot;
                Champion = champ;
            }
        }

        public delegate void OnDanderEventHandler(Obj_AI_Base sender, OnDangerEventArgs args);

        public static event OnDanderEventHandler OnDanger;

        #endregion EventsStuff

        public static void Load()
        {
            Game.OnUpdate += Game_OnUpdate;

            Obj_AI_Base.OnBuffAdd += Obj_AI_Base_OnBuffAdd;
            Obj_AI_Base.OnBuffRemove += Obj_AI_Base_OnBuffRemove;

            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;

            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
        }

        public static Dictionary<string, BuffType> BuffsDictionary = new Dictionary<string, BuffType>();

        private static void Obj_AI_Base_OnBuffAdd(Obj_AI_Base sender, Obj_AI_BaseBuffAddEventArgs args)
        {
            if (sender == null || sender.IsAlly) return;

            if (!BuffsDictionary.ContainsKey(sender.Name))
                BuffsDictionary.Add(sender.Name, args.Buff.Type);
        }

        private static void Obj_AI_Base_OnBuffRemove(Obj_AI_Base sender, Obj_AI_BaseBuffRemoveEventArgs args)
        {
            if (sender == null || sender.IsAlly) return;

            if (BuffsDictionary.ContainsKey(sender.Name))
                BuffsDictionary.Remove(sender.Name);
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (OnDanger == null || Me.IsDead) return;

            foreach (var hero in GameObjects.Heroes)
            {
                var missileInfo = hero.GetMissileInfo();

                if (missileInfo != null)
                {
                    OnDanger.Invoke(hero, missileInfo);
                }
                else
                {
                    var targettedInfo = TargetteSpells.First();
                    if (targettedInfo.Target != null)
                    {
                        
                    }
                }


            }
        }

        #region Extensions

        private static OnDangerEventArgs GetMissileInfo(this Obj_AI_Base target)
        {
            //Missiles
            var missile = Missiles.FirstOrDefault(m => m.IsInRange(target, 3000) && m.IsValid);

            var boundingRadius = target.BoundingRadius + 80;

            var champion = missile?.SpellCaster as Obj_AI_Hero;

            if (champion != null)
            {
                var projection = target.Position
                    .ProjectOn(missile.StartPosition, missile.EndPosition);

                if ((projection.IsOnSegment &&
                     projection.SegmentPoint.Distance(target.Position) <= missile.SData.CastRadius + boundingRadius) ||
                    missile.Target.IsMe)
                {
                    var dangerLevel = 0;

                    switch (missile.Slot)
                    {
                        case SpellSlot.Q:
                            var qDangerLevel =
                                SpellDanger.SpellDangerDB.FirstOrDefault(
                                    dl => dl.QDangerLevel != 0 && dl.Champ == champion.GetChampion());

                            if (qDangerLevel != null) dangerLevel = qDangerLevel.QDangerLevel;
                            break;
                        case SpellSlot.W:
                            var wDangerLevel =
                                SpellDanger.SpellDangerDB.FirstOrDefault(
                                    dl => dl.WDangerLevel != 0 && dl.Champ == champion.GetChampion());

                            if (wDangerLevel != null) dangerLevel = wDangerLevel.WDangerLevel;
                            break;
                        case SpellSlot.E:
                            var eDangerLevel =
                                SpellDanger.SpellDangerDB.FirstOrDefault(
                                    dl => dl.EDangerLevel != 0 && dl.Champ == champion.GetChampion());

                            if (eDangerLevel != null) dangerLevel = eDangerLevel.EDangerLevel;
                            break;
                        case SpellSlot.R:
                            var rDangerLevel =
                                SpellDanger.SpellDangerDB.FirstOrDefault(
                                    dl => dl.RDangerLevel != 0 && dl.Champ == champion.GetChampion());

                            if (rDangerLevel != null) dangerLevel = rDangerLevel.RDangerLevel;
                            break;
                    }

                    if (dangerLevel != 0)
                    {
                        return new OnDangerEventArgs(dangerLevel, missile.Slot, champion.GetChampion());
                    }
                }
            }
            return null;
        }

        #endregion Extensions

        private static List<TargetteSpell> TargetteSpells = new List<TargetteSpell>();

        private struct TargetteSpell
        {
            public Obj_AI_Hero Target;
            public Obj_AI_Hero Sender;
            public int DangerLevel;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var champion = sender as Obj_AI_Hero;
            var target = args.Target as Obj_AI_Hero;

            if (champion == null || target == null || target.IsAlly || OnDanger == null) return;

            var dangerLevel = 0;

            switch (args.Slot)
            {
                case SpellSlot.Q:
                    var qDangerLevel =
                        SpellDanger.SpellDangerDB.FirstOrDefault(
                            dl => dl.QDangerLevel != 0 && dl.Champ == champion.GetChampion());

                    if (qDangerLevel != null) dangerLevel = qDangerLevel.QDangerLevel;
                    break;
                case SpellSlot.W:
                    var wDangerLevel =
                        SpellDanger.SpellDangerDB.FirstOrDefault(
                            dl => dl.WDangerLevel != 0 && dl.Champ == champion.GetChampion());

                    if (wDangerLevel != null) dangerLevel = wDangerLevel.WDangerLevel;
                    break;
                case SpellSlot.E:
                    var eDangerLevel =
                        SpellDanger.SpellDangerDB.FirstOrDefault(
                            dl => dl.EDangerLevel != 0 && dl.Champ == champion.GetChampion());

                    if (eDangerLevel != null) dangerLevel = eDangerLevel.EDangerLevel;
                    break;
                case SpellSlot.R:
                    var rDangerLevel =
                        SpellDanger.SpellDangerDB.FirstOrDefault(
                            dl => dl.RDangerLevel != 0 && dl.Champ == champion.GetChampion());

                    if (rDangerLevel != null) dangerLevel = rDangerLevel.RDangerLevel;
                    break;
            }

            
            if (dangerLevel != 0)
            {
                var targSpell = new TargetteSpell {DangerLevel = dangerLevel, Sender = champion, Target = target};

                TargetteSpells.Add(targSpell);
                DelayAction.Add(args.Start.Distance(args.End) / args.SData.MissileSpeed, () => TargetteSpells.Remove(targSpell));
            }
        }



        #region Missiles Stuff

        private static List<MissileClient> Missiles = new List<MissileClient>();

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var missile = sender as MissileClient;
            if (missile == null || OnDanger == null || sender.IsAlly) return;

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
}
