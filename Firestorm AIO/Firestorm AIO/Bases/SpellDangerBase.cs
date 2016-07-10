using Firestorm_AIO.Enums;
using LeagueSharp.SDK;

namespace Firestorm_AIO.Bases
{
    public class SpellDangerBase
    {
        public Champion Champ;

        public int QDangerLevel;
        public int WDangerLevel;
        public int EDangerLevel;
        public int RDangerLevel;

        public SpellDangerBase(Champion champ)
        {
            Champ = champ;
        }
    }
}
