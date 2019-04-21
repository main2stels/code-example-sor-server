using SorResources.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace siberianoilrush_server.Hosted.Support
{
    public class BuildingThread
    {
        private readonly Thread _updateThread;

        private bool _threadActive;

        private int _millisecondsForSleep;
        private volatile object locked = new object();

        private readonly Action _updateBuildingTime;

        public BuildingThread(Action updateBuildingTime)
        {
            _updateBuildingTime = updateBuildingTime;
            _millisecondsForSleep = 10000;
            _threadActive = true;
            _updateThread = new Thread(new ThreadStart(UpdateBuldingTime));
            _updateThread.Start();
        }

        public void Stop()
            => _threadActive = false;

        public void SetSleep(int millisecondsForSleep)
        {
            lock(locked)
                _millisecondsForSleep = millisecondsForSleep;
        }
        
        private void UpdateBuldingTime()
        {
            while (_threadActive)
            {
                _updateBuildingTime.Invoke();

                Thread.Sleep(_millisecondsForSleep);
            }
        }
    }
}
