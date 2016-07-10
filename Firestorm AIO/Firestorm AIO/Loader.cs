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
using Firestorm_AIO.Helpers;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

using static Firestorm_AIO.Helpers.Helpers;

namespace Firestorm_AIO
{
    internal class Loader
    {
        public static Menu MainMenu;

        public static void Load()
        {
            #region Common Stuff

            MainMenu = new Menu("aioFire" + Me.ChampionName, "Firestorm AIO: " + Me.ChampionName, true).Attach();
            MainMenu.CreateSeparator("a", "Firestorm AIO");
            
            Ultilities.Init.Load();

            AntiGapcloser.Load();

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
