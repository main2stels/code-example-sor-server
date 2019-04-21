using SorResources.Models.Enums;
using SorResources.Models.Interfaces;
using SorResources.Models.Types;

namespace SorResources.Models.Inventory
{
    public abstract class BuildingModel : InventoryModel, IImproving
    {
        public float TimeDismantling { get; set; }

        public float TimeInstalls { get; set; }

        public float TimeFullInstalls { get; set; }

        public float TimeFullDismantling { get; set; }

        public BuildingStatus Status { get; set; }

        public BuildingType Type { get; set; }

        public Position Position { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string StaticId { get; set; }

        public float TimeImproving { get; set; }

        public float TimeFullImproving { get; set; }

        public long EnergyConsumption { get; set; }

        public short Lvl { get; set; }
    }
}
