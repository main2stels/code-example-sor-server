using SorResources.Models.Enums;
using SorResources.Models.Types;
using System.Collections.Generic;

namespace SorResources.Models.Static
{
    public class SBuilding : StaticPurchasable
    {
        public BuildingType Type { get; set; }

        public string Description { get; set; }

        public List<ResourseModel> PricesDismantling { get; set; }

        public ResourceType? TankType { get; set; }

        public List<ResourseModel>[] Lvls { get; set; }
    }
}
