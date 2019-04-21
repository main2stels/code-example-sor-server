using DB.Mongo.Model.Inventory;
using DB.Mongo.Model.Inventory.Buildings;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using siberianoilrush_server.Extension;
using siberianoilrush_server.Service;
using siberianoilrush_server.Service.Inventory;
using SorResources.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace siberianoilrush_server.Hosted
{
    public class ActiveBuildingHostedService : BuildingHostedService, IHostedService, IDisposable
    {
        public ActiveBuildingHostedService(IUserInfoService userInfoService,
            ILoggerService loggerService,
            IBuildingService buildingService) 
            : base(buildingService, loggerService, userInfoService, BuildingStatus.Active) {}

        protected override void CheckBuildings(ref Dictionary<ObjectId, Building> buildings)
        {
            var deleteBuildingList = new List<Building>();

            lock (buildings)
            {
                foreach (var building in buildings)
                {
                    CheckBuilding(building.Value, ref deleteBuildingList);
                }

                foreach (var deleteBuilding in deleteBuildingList)
                {
                    var building = buildings.Where(x => x.Key.Equals(deleteBuilding.Id)).FirstOrDefault();
                    buildings.Remove(building.Key);
                }
            }
        }

        private void CheckBuilding(Building building, ref List<Building> deleteBuilding)
        {
            if(building is DrillingRig)
            {
                var drillingRig = building as DrillingRig;

                var deltaMinute = drillingRig.TimeChangeStatus.GetDeltaMinute(DateTime.Now);

                if(deltaMinute >= drillingRig.TimeDrilling)
                {
                    _buildingService.FinishDrilling(drillingRig);
                    deleteBuilding.Add(drillingRig);
                }
            }
        }

    }
}
