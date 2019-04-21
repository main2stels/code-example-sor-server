using System.ComponentModel;

namespace SorResources.Models.Enums
{
    public enum ResourceType
    {
        [Description("Валюта")]
        Money = 0,

        [Description("Валюта")]
        PremiumMoney = 1,

        [Description("Нефть")]
        Oil = 2,

        [Description("Очищенная нефть")]
        RefinedOil = 3,

        [Description("Бензин")]
        Benzine = 4,

        [Description("Дизель")]
        Diesel = 5,

        [Description("Энергия")]
        Energy = 6
    }
}
