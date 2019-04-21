using SorResources.Models.Types;

namespace SorResources.Models.Inventory.MapObjects
{
    public abstract class MapObject: InventoryModel
    {
        public Position Position { get; set; }
    }
}
