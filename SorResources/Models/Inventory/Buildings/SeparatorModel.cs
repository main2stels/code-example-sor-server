using SorResources.Models.Interfaces;

namespace SorResources.Models.Inventory.Buildings
{
    public class SeparatorModel : BuildingModel, IInstallsBuilding, IExtractorBuilding
    {
        public long SpeedCleaning { get; set; }

        public SeparatorModel() { }

        public SeparatorModel(long speedCleaning)
        {
            SpeedCleaning = speedCleaning;
        }
    }
}
