using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;

namespace Firestorm_AIO.Champions.LeBlanc
{
    internal class LeBlanc :Bases.ChampionBase
    {
        public override void Init()
        {
            Q = new Spell(SpellSlot.Q, 700);
            W = new Spell(SpellSlot.W, 600);
            E = new Spell(SpellSlot.E, 950);
            R = new Spell(SpellSlot.R);

            W.SetSkillshot(0.25f, 220f, 1450f, false, SkillshotType.SkillshotCircle);
            E.SetSkillshot(0.25f, 55f, 1750f, true, SkillshotType.SkillshotLine);
        }

        public override void Menu()
        {
        }

        public override void Active()
        {
        }

        public override void Combo()
        {
        }

        public override void Mixed()
        {
        }

        public override void LaneClear()
        {
        }

        public override void LastHit()
        {
        }

        public override void KillSteal()
        {
        }

        public override void Draw()
        {
        }
    }
}
