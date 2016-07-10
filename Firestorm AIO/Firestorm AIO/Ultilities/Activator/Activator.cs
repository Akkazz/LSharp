using Firestorm_AIO.Helpers;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

using static Firestorm_AIO.Helpers.Helpers;
using static Firestorm_AIO.DataBases.SummonerSpells;

namespace Firestorm_AIO.Ultilities.Activator
{
    public static class Activator
    {
        public static Menu ActivatorMenu;
        public static Menu OffenceMenu;
        public static Menu DefenceeMenu;
        public static Menu QssMenu;

        public static void Load()
        {
            // Init SummonerSpells
            Init();
            /*
            ActivatorMenu = Init.UtilityMenu.CreateMenu("activatormenu", "Activator Menu");
            ActivatorMenu.CreateSeparator("a", ActivatorMenu.DisplayName);

            // SummonerSpells Stuff
            var summonerspells = ActivatorMenu.CreateMenu("summ", "Summoner Spells");
            summonerspells.CreateSeparator("a", summonerspells.DisplayName);

            if (Heal != null)
            {
                var heal = summonerspells.CreateMenu("heal", "Heal Config");
                heal.CreateSeparator("a", heal.DisplayName);
                var healchamps = heal.CreateMenu("champions", "Champion To Use On");
                foreach (var ally in GameObjects.AllyHeroes)
                {
                    healchamps.CreateSliderButton(ally.Name, "HP% To Use On " + ally.ChampionName + " (" + ally.Name + ")", 35);
                }
            }

            if (Barrier != null)
            {
                var barrier = summonerspells.CreateMenu("barrier", "Barrier Config");
                barrier.CreateSeparator("a", barrier.DisplayName);
                barrier.CreateSliderButton("barrier", "HP% To Use", 30);
            }

            if (Ignite != null)
            {
                var ignite = summonerspells.CreateMenu("ignite", "Ignite Config");
                ignite.CreateSeparator("a", ignite.DisplayName);
                ignite.CreateBool("combo", "Use With Combo", false);
                ignite.CreateBool("ks", "Use For KillSteal");

                var ignitechamps = ignite.CreateMenu("enemy", "Champion To Use On");
                foreach (var enemy in GameObjects.EnemyHeroes)
                {
                    ignitechamps.CreateBool(enemy.Name, "Use On " + enemy.ChampionName + " (" + enemy.Name + ")");
                }
            }

            if (Smite != null)
            {
                var smite = summonerspells.CreateMenu("smite", "Smite Config");
                smite.CreateSeparator("a", smite.DisplayName);
                smite.CreateBool("combo", "Use With Combo");
                smite.CreateBool("ks", "Use For KillSteal");

                var smitechamps = smite.CreateMenu("enemy", "Champion To Use On");
                foreach (var enemy in GameObjects.EnemyHeroes)
                {
                    smitechamps.CreateBool(enemy.Name, "Use On " + enemy.ChampionName + " (" + enemy.Name + ")");
                }

                var smitemobs = smite.CreateMenu("mobs", "Jungle Mobs To Use On");
                foreach (var mob in JungleMobs)
                {
                    smitemobs.CreateBool(mob, "Use On " + mob);
                }
            }

            // Offence Items Stuff
            var offence = ActivatorMenu.CreateMenu("offence", "Offence Items");
            offence.CreateSeparator("a", offence.DisplayName);

            var AD = offence.CreateMenu("ad", "AD Items");
            AD.CreateSeparator("a", AD.DisplayName);
            AD.CreateBool("Youmuus", "Use Youmuus");
            AD.CreateBool("Tiamat", "Use Tiamat");
            AD.CreateBool("Hydra", "Use Hydra");
            AD.CreateBool("TitanicHydra", "Use TitanicHydra");

            var botrk = AD.CreateMenu("botrk", "BOTRK Config");
            botrk.CreateSeparator("Cutlass", "Bilgewater Cutlass");
            botrk.CreateSliderButton("CutlassMY", "Use On MY HP%", 75);
            botrk.CreateSliderButton("CutlassENEMY", "Use On ENEMY HP%", 80);
            botrk.CreateSeparator("blade", "Blade of the Ruined King");
            botrk.CreateSliderButton("BotrkMY", "Use On MY HP%", 70);
            botrk.CreateSliderButton("BotrkENEMY", "Use On ENEMY HP%", 75);

            var AP = offence.CreateMenu("ap", "AP Items");
            AP.CreateSeparator("Gunblade", "Hextech Gunblade");
            AP.CreateSliderButton("Hextech_GunbladeMY", "Use On MY HP%", 65);
            AP.CreateSliderButton("Hextech_GunbladeENEMY", "Use On ENEMY HP%", 70);
            AP.CreateSeparator("ProtoBelt", "Hextech ProtoBelt");
            AP.CreateSliderButton("Hextech_ProtoBeltMY", "Use On MY HP%", 65);
            AP.CreateSliderButton("Hextech_ProtoBeltENEMY", "Use On ENEMY HP%", 70);
            AP.CreateSeparator("GLP-800", "Hextech GLP-800");
            AP.CreateSliderButton("GLPMY", "Use On MY HP%", 65);
            AP.CreateSliderButton("GLPENEMY", "Use On ENEMY HP%", 70);

            // Defence Items Stuff
            var defence = ActivatorMenu.CreateMenu("defence", "Defence Items");
            defence.CreateSeparator("a", defence.DisplayName);
            defence.CreateSliderButton("Zhonyas", "Use Zhonyas On MY HP%", 25);
            defence.CreateSliderButton("Solari", "Use Solari On MY HP%", 45);
            defence.CreateSliderButton("Randuins", "Use Randuins On MY HP%", 75);

            // Potions Items Stuff
            var Potions = ActivatorMenu.CreateMenu("potions", "Potions Items");
            Potions.CreateSeparator("a", Potions.DisplayName);
            Potions.CreateSliderButton("Health_Potion", "Use Health Potion On MY HP%", 45);
            Potions.CreateSliderButton("Biscuit", "Use Biscuit On MY HP%", 40);
            Potions.CreateSliderButton("RefillablePotion", "Use Refillable Potion On MY HP%", 70);
            Potions.CreateSliderButton("CorruptingPotion", "Use Corrupting Potion On MY HP%", 60);
            Potions.CreateSliderButton("HuntersPotion", "Use Hunters Potion On MY HP%", 70);

            // QSS Items Stuff
            QssMenu = ActivatorMenu.CreateMenu("qss", "Cleanes Menu");
            QssMenu.CreateSeparator("a", QssMenu.DisplayName);

            var debuffslist = QssMenu.CreateMenu("debuffs", "Debuffs To Clean");
            debuffslist.CreateSeparator("a", "DeBuffs List");
            foreach (var debuff in DeBuffs)
            {
                debuffslist.CreateBool(debuff.Key.ToString(), "Use On " + debuff.Key, debuff.Value);
            }

            var items = QssMenu.CreateMenu("item", "Spells To Use");
            items.CreateSeparator("a", "Spells List");
            items.CreateBool("Quicksilver_Sash", "Use Quicksilver Sash");
            items.CreateBool("Mercurial_Scimitar", "Use Mercurial Scimitar");
            items.CreateBool("Dervish_Blade", "Use Dervish Blade");
            items.CreateBool("Mikaels", "Use Mikaels");
            if (Cleanse != null)
            {
                items.CreateBool("Cleanse", "Use Cleanse");
            }

            var heros = QssMenu.CreateMenu("ally", "Champion To Use On");
            heros.CreateSeparator("a", "Champions List");
            foreach (var hero in GameObjects.AllyHeroes)
            {
                heros.CreateSliderButton(hero.Name, "HP% To Use On " + hero.ChampionName + " (" + hero.Name + ")", 50);
            }
            */
        }
    }
}
