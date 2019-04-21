using SorResources.Models.Types;

namespace SorResources.Models.Inventory
{
    public class OilZoneModel
    {
        public string Id { get; set; }

        public Position Position { get; set; }

        public float Rotation { get; set; }
        
        public int PrefabNumber { get; set; }

        public long Value { get; set; }

        public bool IsUses { get; set; }

        public bool IsEmpty { get; set; }
    }
}