using System;

namespace de.springwald.blazortools.Services.waiting
{
    public class WaitingData
    {
        public string FullscreenInfoTitle;
        public string FullscreenInfoMessage;
        public int NumberOfFullScreenWaiting;
        public int NumberOfLocalWaiting;
    }

    public interface IWaitingService
    {
        event EventHandler<WaitingData> Changed;

        WaitingData Data { get; }

        void StartLocalWaitingInstance();

        void StopLocalWaitingInstance();

        void StartFullScreenWaitingInstance(string infoMessage, string title = null);

        void StopFullScreenWaitingInstance();

        void StopAll();
    }
}
