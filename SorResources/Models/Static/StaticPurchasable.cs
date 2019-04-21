using SorResources.Models.Types;
using System.Collections.Generic;

namespace SorResources.Models.Static
{
    public abstract class StaticPurchasable : StaticModel
    {
        public string Name { get; set; }

        public List<ResourseModel> Price { get; set; }
    }
}
