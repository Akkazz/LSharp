using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firestorm_AIO.Champions.Illaoi;
using Firestorm_AIO.Champions.Rumble;
using Firestorm_AIO.Champions.Yasuo;
using LeagueSharp;

namespace Firestorm_AIO
{
    internal class Loader
    {
        public static void Load()
        {
            #region Common Stuff

            Helpers.AntiGapcloser.Load();

            #endregion Common Stuff

            switch (ObjectManager.Player.ChampionName)
            {
                case "Yasuo":
                    new Yasuo().Load();
                    break;
                case "Rumble":
                    new Rumble().Load();
                    break;
                case "Illaoi":
                    new Illaoi().Load();
                    break;
                //TODO
                case "Anivia":
                    break;
                case "Gnar":

                    break;
                case "Bard":

                    break;
                case "Ahri":

                    break;
                case "Ezreal":

                    break;
                default:
                    Game.PrintChat("Champion not supported in FireStorm AIO");
                    break;
            }
        }
    }
}
