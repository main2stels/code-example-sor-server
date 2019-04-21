using SorResources.Models.Inventory;

namespace SorResources.Models
{
    public sealed class OilfieldModel
    {
        public string Id { get; set; }

        public TreesModel Trees { get; set; }

        public OilZoneModel[] OilZones { get; set; }
    }
}
