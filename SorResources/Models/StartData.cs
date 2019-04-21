using SorResources.Models.Inventory;
using SorResources.Models.Inventory.MapObjects;
using SorResources.Models.Static;
using SorResources.Models.Types;
using System.Collections.Generic;

namespace SorResources.Models
{
    public sealed class StartData
    {
        public WalletModel[] Wallets { get; set; }

        public OilfieldModel OilfieldData { get; set; }

        public UserInfoModel UserInfo { get; set; }

        public BuildingModel[] Buildings { get; set; }

        public SBuilding[] SBuilding { get; set; }

        public SquadModel[] Squads { get; set; }

        public SSquad[] SSquads { get; set; }

        public SharingResources[] SharingResources { get; set; }

        public OilWell [] OilWells { get; set; }

        public EmptyHole[] EmptyHoles { get; set; }

        public ResourseModel[] Limits { get; set; }

        public bool IsDeveloper { get; set; }
    }
}
