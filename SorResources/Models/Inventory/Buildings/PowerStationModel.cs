using SorResources.Models.Interfaces;

namespace SorResources.Models.Inventory.Buildings
{
    public sealed class PowerStationModel : BuildingModel, IInstallsBuilding, IGeneratorEnergy, IExtractorBuilding
    {
        public long GeneratedEnergy { get; set; }

        public long ConsumptionDiesel { get; set; }
    }
}
