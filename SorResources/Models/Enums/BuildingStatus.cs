using System.ComponentModel;

namespace SorResources.Models.Enums
{
    public enum BuildingStatus
    {
        [Description("Placed and working right now")]
        Active = 0,

        [Description("Not placed, not working")]
        Inactive = 1,

        [Description("Placed, but not working")]
        Chill = 2,

        [Description("Placed, not working, but grading")]
        Grading = 3,

        [Description("Non placeable and cannot be graded")]
        Blocked = 4,

        [Description("Construction")]
        Construction = 5,

        [Description("Dismantling")]
        Dismantling = 6
    }
}
