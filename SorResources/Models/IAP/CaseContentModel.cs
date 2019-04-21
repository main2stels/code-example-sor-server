using System;

namespace SorResources.Models.IAP
{
    public sealed class CaseContentModel
    {
        public CaseItemModel[] Items { get; set; }

        public CaseContentModel() { }

        public CaseContentModel(CaseItemModel[] caseItemModels)
        {
            if (caseItemModels.Length != 4)
                throw new Exception("Case item != 4");

            Items = caseItemModels;
        }
    }
}
