using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.SDK;

namespace Firestorm_AIO
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bootstrap.Init();

            Events.OnLoad += Events_OnLoad;
        }

        private static void Events_OnLoad(object sender, EventArgs e)
        {
            Loader.Load();
        }
    }
}
