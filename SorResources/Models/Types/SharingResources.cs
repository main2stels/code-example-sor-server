using SorResources.Models.Enums;

namespace SorResources.Models.Types
{
    public class SharingResources
    {
        public ResourceType Resource { get; set; }

        public ResourseModel PricePurchase { get; set; }

        public ResourseModel PriceSell { get; set; }
    }
}
