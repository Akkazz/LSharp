using System;
using Firestorm_AIO.Enums;
using LeagueSharp;

namespace Firestorm_AIO.DataBases
{
    public static class Champions
    {
        private static T ToEnum<T>(this string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        public static Champion GetChampion(this Obj_AI_Hero hero)
        {
            //Special Cases
            switch (hero.ChampionName)
            {
                //TODO FIX ALL EXCEPTIONS
                case "Aurelion Sol":
                    return Champion.AurelionSol;
                case "Cho'Gath":
                    return Champion.ChoGath;
                case "Dr Mundo":
                    return Champion.DrMundo;
                case "Kha'Zix":
                    return Champion.KhaZix;
                case "Kog'Maw":
                    return Champion.KogMaw;
                case "Le Blanc":
                    return Champion.LeBlanc;
                case "Lee Sin":
                    return Champion.LeeSin;
                case "Master Yi":
                    return Champion.MasterYi;
                case "Miss Fortune":
                    return Champion.MissFortune;
                case "Tahm Kench":
                    return Champion.TahmKench;
                case "Rek'Sai":
                    return Champion.RekSai;
                case "Twisted Fate":
                    return Champion.TwistedFate;
                case "Jarvan IV":
                    return Champion.JarvanIV;

            }

            return hero.ChampionName.ToEnum<Champion>();
        }
    }
}