using System;
using Firestorm_AIO.Bases;
using LeagueSharp;

namespace Firestorm_AIO.Champions.Anivia
{
    public class ObjManager
    {
        public static MyObjectBase QObject;
        public static MyObjectBase RObject;

        public static void Load()
        {
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name == "cryo_FlashFrost_Player_mis.troy" && sender.IsAlly)
            {
                QObject = new MyObjectBase(sender.Position);
            }

            if (sender.Name == "cryo_storm" && sender.IsAlly)
            {
                RObject = new MyObjectBase(sender.Position);
            }
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            if (sender.Name == "cryo_FlashFrost_Player_mis.troy" && sender.IsAlly)
            {
                QObject = null;
            }

            if (sender.Name.Contains("cryo_storm") && sender.IsAlly)
            {
                RObject = null;
            }
        }
    }
}
