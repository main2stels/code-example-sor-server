using DB.Mongo.Model.Inventory;
using DB.Mongo.Model.Inventory.Buildings;
using siberianoilrush_server.Extension;
using siberianoilrush_server.Service.Inventory;
using SorResources.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace siberianoilrush_server.Hosted.Support
{
    public class BuildingHosted : IDisposable
    {
        private Building _building;
        private readonly IBuildingService _buildingService;
        private readonly Dictionary<BuildingStatus, Action> _statusActions;

        private volatile BuildingThread _updateThread;                

        public BuildingHosted(Building building, IBuildingService buildingService)
        {
            _building = building;
            _buildingService = buildingService;

            _statusActions = new Dictionary<BuildingStatus, Action>
            {
                { BuildingStatus.Active, ActiveBuilding },
                { BuildingStatus.Dismantling, DismantlingBuilding },
                { BuildingStatus.Construction, ConstructionBuilding },
            };

            InitThread();
        }

        public void UpdateBuilding(Building building)
        {
            lock(_building)
            {
                if (!building.Id.Equals(_building.Id))
                    throw new ArgumentException();

                _building = building;
                InitThread();
            }
        }

        private void UpdateBuildingTime()
        {
            lock (_building)
            {
                Action action;
                if (_statusActions.TryGetValue(_building.Status, out action))
                    action.Invoke();
                else
                    _updateThread?.SetSleep(10000);
            }
        }

        private void InitThread()
        {
            if (_updateThread != null)
                _updateThread.Stop();

            _updateThread = new BuildingThread(UpdateBuildingTime);
        }      

        private void ActiveBuilding()
        {
            if (_building is DrillingRig)
            {
                DrillingRigActive();
            }
        }

        private void DismantlingBuilding()
        {
            var timeStartInstalls = _building.TimeChangeStatus;
            var deltaMinute = timeStartInstalls.GetDeltaMinute(DateTime.Now);

            if (deltaMinute >= _building.TimeDismantling)
            {
                lock (_buildingService)
                {
                    _building = _buildingService.FinishDismantling(_building.Id);
                    _updateThread.SetSleep(100);
                }
            }
            else
            {
                SetNextSleep(_building.TimeDismantling, deltaMinute);
            }
        }

        private void ConstructionBuilding()
        {
            var timeStartInstalls = _building.TimeStartInstalls ?? throw new Exception();
            var deltaMinute = timeStartInstalls.GetDeltaMinute(DateTime.Now);

            if (deltaMinute >= _building.TimeInstalls)
            {
                lock (_buildingService)
                {
                    _buildingService.FinishBuilding(_building.Id);
                    _updateThread.SetSleep(100);
                }
            }
            else
            {
                SetNextSleep(_building.TimeDismantling, deltaMinute);
            }
        }

        public void Dispose()
        {
            _updateThread.Stop();
        }

        private void SetNextSleep(float time, float deltaMinute)
        {
            var nextSleep = (time - deltaMinute) * 0.6f;
            int millisecondsForSleep = (int)(nextSleep * 60000);

            if (millisecondsForSleep < 500)
                millisecondsForSleep = 500;

            _updateThread.SetSleep(millisecondsForSleep);
        }

        private void DrillingRigActive()
        {
            var drillingRig = _building as DrillingRig;

            var deltaMinute = drillingRig.TimeChangeStatus.GetDeltaMinute(DateTime.Now);

            if (deltaMinute >= drillingRig.TimeDrilling)
            {
                lock (_buildingService)
                {
                    _building = _buildingService.FinishDrilling(drillingRig);
                    _updateThread.SetSleep(10);
                }
            }
            else
            {
                SetNextSleep(drillingRig.TimeDrilling, deltaMinute);
            }
        }
    }
}
