using System.ComponentModel;

namespace SorResources.Models.Enums
{
    public enum BuildingType
    {
        [Description("Needs to find oil zone for drilling rig")]
        ResearchRig = 0,

        [Description("Needs to prepare place for rocking rig")]
        DrillingRig = 1,

        [Description("Needs to produce oil")]
        RockingRig = 2,

        [Description("Needs to store oil")]
        Tank = 3,

        [Description("Needs to refine oil")]
        Separator = 4,

        [Description("Needs to process oil to benzine and diesel")]
        Refinery = 5,

        [Description("Needs to produce electric power from diesel")]
        PowerStation = 6,

        [Description("Needs to make trading operations")]
        TradingHouse = 7,

        [Description("Needs to build, improve and etc.")]
        TownHall = 8,

        [Description("Needs to make army and prepare it for pvp")]
        Barracks = 9
    }
}
