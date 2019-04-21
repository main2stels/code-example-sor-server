using SorResources.Models.Interfaces;

namespace SorResources.Models.Inventory.Buildings
{
    public sealed class RockingRigModel : BuildingModel, IInstallsBuilding, IExtractorBuilding
    {
        public float SpeedExtraction { get; set; }
    }
}
