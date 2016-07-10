using Firestorm_AIO.Helpers;
using LeagueSharp;
using Menu = LeagueSharp.SDK.UI.Menu;
using static Firestorm_AIO.Helpers.Helpers;
namespace Firestorm_AIO.Ultilities
{
    class Init
    {
        public static Menu UtilityMenu;

        public static void Load()
        {
            switch (Game.MapId)
            {
                case GameMapId.SummonersRift:
                    JungleMobs = new[]
                                     {
                                             "SRU_Dragon_Air", "SRU_Dragon_Earth", "SRU_Dragon_Fire", "SRU_Dragon_Water", "SRU_Dragon_Elder", "SRU_Baron", "SRU_Gromp", "SRU_Krug", "SRU_Razorbeak",
                                             "SRU_RiftHerald", "Sru_Crab", "SRU_Murkwolf", "SRU_Blue", "SRU_Red"
                                         };
                    break;
                case GameMapId.TwistedTreeline:
                    JungleMobs = new[] { "TT_NWraith", "TT_NWolf", "TT_NGolem", "TT_Spiderboss" };
                    break;
                case GameMapId.CrystalScar:
                    JungleMobs = new[] { "AscXerath" };
                    break;
                default:
                    JungleMobs = new[] { "Nothing" };
                    break;
            }

            UtilityMenu = Loader.MainMenu.Add(new Menu("utility", "Ultilities Menu"));
            UtilityMenu.CreateSeparator("a", "Ultilities");
            Activator.Activator.Load();
            Tracker.Tracker.Load();
        }
    }
}
