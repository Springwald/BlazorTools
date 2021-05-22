// A small collection of everyday components and services for dotnet Blazor
// https://github.com/Springwald/BlazorTools
//
// (C) 2021 Daniel Springwald, Bochum Germany
// Springwald Software  -   www.springwald.de
// daniel@springwald.de -  +49 234 298 788 46
// All rights reserved
// Licensed under MIT License

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
