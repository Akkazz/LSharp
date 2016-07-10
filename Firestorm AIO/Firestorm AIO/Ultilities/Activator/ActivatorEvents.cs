using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;

using static Firestorm_AIO.Helpers.Helpers;

namespace Firestorm_AIO.Ultilities.Activator
{
    internal class ActivatorEvents
    {
        #region EventsStuff
        public delegate void OnDangerEventHandler(Obj_AI_Base sender, EventArgs args);

        public static event OnDangerEventHandler OnDanger;
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

        private static void Obj_AI_Base_OnBuffRemove(Obj_AI_Base sender, Obj_AI_BaseBuffRemoveEventArgs args)
        {
            if (!sender.IsAlly || sender == null) return;

            if (BuffsDictionary.ContainsKey(sender.Name))
                BuffsDictionary.Remove(sender.Name);
        }

        private static void Obj_AI_Base_OnBuffAdd(Obj_AI_Base sender, Obj_AI_BaseBuffAddEventArgs args)
        {
            if(!sender.IsAlly || sender == null) return;

            if(!BuffsDictionary.ContainsKey(sender.Name))
            BuffsDictionary.Add(sender.Name, args.Buff.Type);
        }

        private static void Game_OnUpdate(EventArgs args)
        {
           if(OnDanger == null || Me.IsDead)return;

        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if(sender == null || OnDanger == null || Me.IsDead) return;
        }

        #region Missiles Stuff

        private static List<GameObject> Missiles = new List<GameObject>();

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var missile = sender as MissileClient;
            if (missile == null || OnDanger == null || Me.IsDead || sender.IsAlly) return;

            var senderHero = missile.SpellCaster as Obj_AI_Hero;
            if (senderHero == null) return;

            Missiles.Add(sender);
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            var missile = sender as MissileClient;
            if (missile == null || OnDanger == null || sender.IsAlly) return;

            var senderHero = missile.SpellCaster as Obj_AI_Hero;
            if (senderHero == null) return;

            Missiles.Remove(sender);
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
