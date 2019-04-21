using SorResources.Models.Interfaces;

namespace SorResources.Models.Inventory.Buildings
{
    public sealed class DrillingRigModel : BuildingModel, IInstallsBuilding
    {
        public float TimeDrilling { get; set; }       

        public float TimeFullDrilling { get; set; }

        public string MapObjectId { get; set; }
    }
}
