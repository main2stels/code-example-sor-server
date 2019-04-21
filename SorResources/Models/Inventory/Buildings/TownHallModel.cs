using SorResources.Models.Interfaces;

namespace SorResources.Models.Inventory.Buildings
{
    public sealed class TownHallModel : BuildingModel, IGeneratorEnergy
    {
        public float StorageСapacity { get; set; }

        public long GeneratedEnergy { get; set; }

        public long ConsumptionDiesel { get; set; }
    }
}
