﻿using System;

namespace de.springwald.blazortools.Services
{
    public class WaitingService
    {
        public class WaitingData
        {
            public string FullscreenInfoTitle;
            public string FullscreenInfoMessage;
            public int NumberOfFullScreenWaiting;
            public int NumberOfLocalWaiting;
        }

        public event EventHandler<WaitingData> Changed; // event

        public readonly WaitingData Data;

        public WaitingService()
        {
            this.Data = new WaitingData();
        }

        public void StartLocalWaitingInstance()
        {
            this.SetNumberOfLocalWaiting(this.Data.NumberOfLocalWaiting + 1);
        }

        public void StopLocalWaitingInstance()
        {
            this.SetNumberOfLocalWaiting(this.Data.NumberOfLocalWaiting - 1);
        }

        public void StartFullScreenWaitingInstance(string infoMessage, string title = null)
        {
            this.Data.FullscreenInfoTitle = title ?? "...just a moment...";
            this.Data.FullscreenInfoMessage = infoMessage;

            this.SetNumberOfFullScreenWaiting(this.Data.NumberOfFullScreenWaiting + 1);
            this.StartLocalWaitingInstance();
        }

        public void StopFullScreenWaitingInstance()
        {
            this.SetNumberOfFullScreenWaiting(this.Data.NumberOfFullScreenWaiting - 1);
            this.StopLocalWaitingInstance();
        }

        public void StopAll()
        {
            this.SetNumberOfLocalWaiting(0, throwChangedWhenChanged: false);
            this.SetNumberOfFullScreenWaiting(0, throwChangedWhenChanged: false);
            if (this.Changed != null) this.Changed.Invoke(this, this.Data);
        }

        private void SetNumberOfFullScreenWaiting(int value, bool throwChangedWhenChanged = true)
        {
            int lastValue = this.Data.NumberOfFullScreenWaiting;
            if (value < 0) value = 0;
            if (throwChangedWhenChanged && value != lastValue)
            {
                this.Data.NumberOfFullScreenWaiting = value;
                this.Changed?.Invoke(this, this.Data);
            }
        }

        private void SetNumberOfLocalWaiting(int value, bool throwChangedWhenChanged = true)
        {
            var lastValue = this.Data.NumberOfLocalWaiting;
            if (value < 0) value = 0;
            if (throwChangedWhenChanged && value != lastValue)
            {
                this.Data.NumberOfLocalWaiting = value;
                this.Changed?.Invoke(this, this.Data);
            }
        }
    }

}
