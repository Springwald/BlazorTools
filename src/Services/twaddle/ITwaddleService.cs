// A small collection of everyday components and services for dotnet Blazor
// https://github.com/Springwald/BlazorTools
//
// (C) 2021 Daniel Springwald, Bochum Germany
// Springwald Software  -   www.springwald.de
// daniel@springwald.de -  +49 234 298 788 46
// All rights reserved
// Licensed under MIT License

using System;
using System.Threading.Tasks;

namespace de.springwald.blazortools.Services.twaddle
{
    public enum TwaddleTypes
    {
        Info,
        Success,
        Error,
        Warning,
        LogOnly
    }

    public class TwaddleMessage
    {
        public TwaddleTypes Typ { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string UltraDetailedMessage { get; set; }
        public DateTime Time { get; set; }
        public bool Seen { get; set; }
    }

    public interface ITwaddleService
    {
        event EventHandler<int> NumberUnseenTwaddlesChanged;
        event EventHandler<TwaddleMessage> NewTwaddle;
        event EventHandler<string> ErrorThrown;

        TwaddleMessage[] GetAllNotifications();
        TwaddleMessage[] GetAllEvents();

        void SetAllNotificationsAsSeen();

        /// <summary>
        /// <param name="throwGlobalError">Throw the error up through the complete application. It is then usually also displayed by the browser.</param>
        /// <returns></returns>
        Task AddError(string title, string message, string messageUltraDetailed, bool throwGlobalError);
        Task AddWarning(string title, string message, string messageUltraDetailed);
        Task AddLogOnly(string title, string message, string messageUltraDetailed);
        Task AddSuccess(string title, string message, string messageUltraDetailed);
        Task AddInfo(string title, string message, string messageUltraDetailed);
    }

   
}
