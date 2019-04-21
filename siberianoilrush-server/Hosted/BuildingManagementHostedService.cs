using DB.Mongo.Model.Inventory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using siberianoilrush_server.Hosted.Support;
using siberianoilrush_server.Service;
using siberianoilrush_server.Service.Inventory;
using SorResources.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace siberianoilrush_server.Hosted
{
    public class BuildingManagementHostedService : IHostedService
    {
        private readonly BuildingService _buildingService;
        private readonly Dictionary<ObjectId, BuildingHosted> _buildingHosteds;
        private readonly LoggerService _loggerService;

        public BuildingManagementHostedService(BuildingService buildingService,
            LoggerService loggerService)
        {
            _buildingHosteds = new Dictionary<ObjectId, BuildingHosted>();
            _buildingService = buildingService;
            _loggerService = loggerService;

            var buildings = _buildingService.GetAll().Where(x => x.Status != BuildingStatus.Inactive);

            foreach(var building in buildings)
            {
                var buildingHosted = new BuildingHosted(building, buildingService);

                _buildingHosteds.Add(building.Id, buildingHosted);
            }

            BuildingService.UpdateBuildingEvent += UpdateBuildingHandler;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _loggerService.Log($"BuildingManeger Start:{DateTime.Now}",
                null, Microsoft.Extensions.Logging.LogLevel.Information);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _loggerService.Log($"BuildingManeger Stop:{DateTime.Now}",
                null, Microsoft.Extensions.Logging.LogLevel.Information);
            return Task.CompletedTask;
        }

        public void UpdateBuildingHandler(Building building)
        {
            if(!_buildingHosteds.ContainsKey(building.Id))
            {
                if(building.Status != BuildingStatus.Chill || building.Status != BuildingStatus.Inactive)
                    _buildingHosteds.Add(building.Id, new BuildingHosted(building, _buildingService));                    
            }
            else
            {
                if (building.Status == BuildingStatus.Chill || building.Status == BuildingStatus.Inactive)
                {
                    _buildingHosteds[building.Id].Dispose();
                    _buildingHosteds.Remove(building.Id);
                }
                else
                    _buildingHosteds[building.Id].UpdateBuilding(building);
            }
        }
    }
}
