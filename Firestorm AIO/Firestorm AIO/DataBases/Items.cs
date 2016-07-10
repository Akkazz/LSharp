using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using static LeagueSharp.SDK.Items;

namespace Firestorm_AIO.DataBases
{
    public static class Items
    {
        #region WardingItems

        public static Item EyeOfTheEquinox = new Item(2303, 600);
        public static Item EyeOfTheOasis = new Item(2302, 600);
        public static Item EyeOfTheWatchers = new Item(2301, 600);
        public static Item TrackerKnife = new Item(ItemId.Poachers_Knife, 600);
        public static Item TrackerKnife_Devourer = new Item(ItemId.Poachers_Knife_Enchantment_Devourer, 600);
        public static Item TrackerKnife_Juggernaut = new Item(ItemId.Poachers_Knife_Enchantment_Juggernaut, 600);
        public static Item TrackerKnife_Magus = new Item(ItemId.Poachers_Knife_Enchantment_Magus, 600);
        public static Item TrackerKnife_Warrior = new Item(ItemId.Poachers_Knife_Enchantment_Warrior, 600);
        public static Item SightStone = new Item(ItemId.Sightstone, 600);
        public static Item RubySightStone = new Item(ItemId.Ruby_Sightstone, 600);
        public static Item VisionWard = new Item(ItemId.Vision_Ward, 600);
        public static Item YellowTrinket = new Item(ItemId.Warding_Totem_Trinket, 600);
        public static Item BlueTrinket = new Item(ItemId.Farsight_Orb_Trinket, 600);
        public static Item RedTrinket = new Item(ItemId.Sweeping_Lens_Trinket, 600);
        public static Item RedTrinket2 = new Item(ItemId.Oracles_Lens_Trinket, 600);

        #endregion WardingItems

        #region QssItems

        public static Item Mercurial_Scimitar = new Item(ItemId.Mercurial_Scimitar, 0);
        public static Item Quicksilver_Sash = new Item(ItemId.Quicksilver_Sash, 0);
        public static Item Dervish_Blade = new Item(ItemId.Dervish_Blade, 0);
        public static Item Mikaels = new Item(ItemId.Mikaels_Crucible, 600);

        #endregion QssItems

        #region SupportItems

        public static Item Talisman = new Item(ItemId.Talisman_of_Ascension, 700);
        public static Item FrostQueen = new Item(ItemId.Frost_Queens_Claim, 2000);
        public static Item FOTM = new Item(ItemId.Face_of_the_Mountain, 600);

        #endregion SupportItems

        #region ADItems

        public static Item Youmuus = new Item(ItemId.Youmuus_Ghostblade, 0);
        public static Item Cutlass = new Item(ItemId.Bilgewater_Cutlass, 550);
        public static Item Botrk = new Item(ItemId.Blade_of_the_Ruined_King, 550);
        public static Item Tiamat = new Item(ItemId.Tiamat_Melee_Only, 200);
        public static Item Hydra = new Item(ItemId.Ravenous_Hydra_Melee_Only, 200);
        public static Item TitanicHydra = new Item(3748, 200);

        #endregion ADItems

        #region APItems
        
        public static Item Hextech_Gunblade = new Item(ItemId.Hextech_Gunblade, 600);
        public static Item Hextech_ProtoBelt = new Item(ItemId.Will_of_the_Ancients, 600);
        public static Item Hextech_GLP = new Item(3030, 600);

        #endregion APItems

        #region Potions

        public static Item HealthPotion = new Item(ItemId.Health_Potion, 0);
        public static Item Biscuit = new Item(2010, 0);
        public static Item RefillablePotion = new Item(2031, 0);
        public static Item CorruptingPotion = new Item(2033, 0);
        public static Item HuntersPotion = new Item(2032, 0);

        #endregion Potions

        #region TankItems

        public static Item Zhonyas = new Item(ItemId.Zhonyas_Hourglass, 0);
        public static Item Solari = new Item(ItemId.Locket_of_the_Iron_Solari, 600);
        public static Item Randuins = new Item(ItemId.Randuins_Omen, 450);

        #endregion TankItems
    }
}
