using SorResources.Models.Enums;

namespace SorResources.Models.Static
{
    public class SSquad : StaticPurchasable
    {
        public SquadType Type { get; set; }

        public float Speed { get; set; }

        public float Damage { get; set; }

        public float AttackRange { get; set; }

        public float Hp { get; set; }

        public float AgrDistance { get; set; }

        public short UnitCount { get; set; }
    }
}
