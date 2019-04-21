using SorResources.Models.Enums;

namespace SorResources.Models.Inventory
{
    public class SquadModel : InventoryModel
    {
        public float Hp { get; set; }

        public short UnitCount { get; set; }

        public SquadType Type { get; set; }
    }
}
