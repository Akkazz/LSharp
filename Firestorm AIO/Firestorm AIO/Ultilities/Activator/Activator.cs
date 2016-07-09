using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firestorm_AIO.DataBases;

namespace Firestorm_AIO.Ultilities.Activator
{
    class Activator
    {
        public static void Load()
        {
            SummonerSpells.Init();
        }
    }
}
