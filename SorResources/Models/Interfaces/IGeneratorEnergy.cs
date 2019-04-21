namespace SorResources.Models.Interfaces
{
    public interface IGeneratorEnergy
    {
        long GeneratedEnergy { get; set; }

        long ConsumptionDiesel { get; set; }
    }
}
