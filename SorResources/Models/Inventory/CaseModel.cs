namespace SorResources.Models.Inventory
{
    public sealed class CaseModel
    {
        public string Type { get; set; }

        public int Count { get; set; }

        public CaseModel(string type, int count)
        {
            Type = type;
            Count = count;
        }
    }
}
