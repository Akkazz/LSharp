using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.SDK;

using static Firestorm_AIO.Helpers.Helpers;

namespace Firestorm_AIO.DataBases
{
    class SummonerSpells
    {
        public static Spell Flash;
        public static Spell Ignite;
        public static Spell Heal;
        public static Spell Smite;
        public static Spell Ghost;
        public static Spell Barrier;
        public static Spell Cleanse;
        public static Spell Clarity;
        public static Spell Mark;
        public static Spell Exhaust;
        public static Spell Teleport;

        public static void Init()
        {
            var flash = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerflash"));
            var ignite = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerdot"));
            var heal = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerheal"));
            var smite = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("smite"));
            var ghost = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerhaste"));
            var barrier = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerbarrier"));
            var cleanse = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerboost"));
            var clarity = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonermana"));
            var mark = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonersnowball"));
            var exhaust = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerexhaust"));
            var teleport = Me.Spellbook.Spells.FirstOrDefault(s => s.Name.ToLower().Contains("summonerteleport"));

            if (flash != null)
            {
               Flash = new Spell(flash.Slot, 450);
            }
            if (ignite != null)
            {
                Ignite = new Spell(ignite.Slot, 550);
            }
            if (heal != null)
            {
                Heal = new Spell(heal.Slot, 700);
            }
            if (smite != null)
            {
                Smite = new Spell(smite.Slot, 550);
            }
            if (ghost != null)
            {
                Ghost = new Spell(ghost.Slot, 800);
            }
            if (barrier != null)
            {
                Barrier = new Spell(barrier.Slot, 1000);
            }
            if (cleanse != null)
            {
                Cleanse = new Spell(cleanse.Slot, 1000);
            }
            if (clarity != null)
            {
                Clarity = new Spell(clarity.Slot, 700);
            }
            if (mark != null)
            {
                Mark = new Spell(mark.Slot, 1000);
            }
            if (exhaust != null)
            {
                Exhaust = new Spell(exhaust.Slot, 1000);
            }
            if (teleport != null)
            {
                Teleport = new Spell(teleport.Slot, int.MaxValue);
            }
        }
    }
}
