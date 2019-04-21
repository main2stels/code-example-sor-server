using SorResources.Models.Enums;

namespace SorResources.Models.Types
{
    public class ResourseModel
    {
        public ResourceType Type { get; set; }

        public long Amount { get; set; }

        public ResourseModel() { }

        public ResourseModel(ResourceType type, long amount)
        {
            Type = type;
            Amount = amount;
        }

        public ResourseModel Clone()
            => new ResourseModel(Type, Amount);
    }
}
