using SorResources.Models.Enums;
using SorResources.Models.Interfaces;

namespace SorResources.Models.Inventory.Buildings
{
    public class TankModel: BuildingModel, IInstallsBuilding
    {
        public ResourceType Resource { get; set; }

        public float StorageСapacity { get; set; }

        public TankModel() { }

        public TankModel(ResourceType resourceTank, float storageСapacity)
        {
            Resource = resourceTank;
            StorageСapacity = storageСapacity;
        }
    }
}
