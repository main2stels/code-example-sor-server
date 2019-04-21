using SorResources.Models.Interfaces;

namespace SorResources.Models.Inventory.Buildings
{
    public class RefineryModel : BuildingModel, IInstallsBuilding, IExtractorBuilding
    {
        public long DiezelGenerate { get; set; }

        public long BenzineGenerate { get; set; }

        public long ConsumptionRefinedOil { get; set; }

        public RefineryModel() { }

        public RefineryModel(long diezelGenerate, long benzineGenerate, long consumptionRefinedOil)
        {
            DiezelGenerate = diezelGenerate;
            BenzineGenerate = benzineGenerate;
            ConsumptionRefinedOil = consumptionRefinedOil;
        }
    }
}
