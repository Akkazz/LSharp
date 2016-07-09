using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firestorm_AIO.Champions.Anivia;
using Firestorm_AIO.Champions.Illaoi;
using Firestorm_AIO.Champions.LeBlanc;
using Firestorm_AIO.Champions.NidaLee;
using Firestorm_AIO.Champions.Rumble;
using Firestorm_AIO.Champions.Yasuo;
using LeagueSharp;
using LeagueSharp.SDK;

namespace Firestorm_AIO
{
    internal class Loader
    {
        public static void Load()
        {
            #region Common Stuff

            Ultilities.Activator.Activator.Load();
            Ultilities.Tracker.Tracker.Load();

            Helpers.AntiGapcloser.Load();

            #endregion Common Stuff

            switch (ObjectManager.Player.ChampionName)
            {
                case "Anivia":
                    new Anivia().Load();
                    break;

                case "Nidalee":
                    new NidaLee().Load();
                    break;

                case "LeBlanc":
                    new LeBlanc().Load();
                    break;

                case "Rumble":
                    new Rumble().Load();
                    break;

                case "Illaoi":
                    new Illaoi().Load();
                    break;

                case "Yasuo":
                    new Yasuo().Load();
                    break;
                //TODO
                case "Gnar":

                    break;
                case "Bard":

                    break;
                case "Ahri":

                    break;
                case "Ezreal":

                    break;
                default:
                    Game.PrintChat(GameObjects.Player.ChampionName + " not supported in FireStorm AIO");
                    break;
            }
        }
    }
}
