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

        Task AddError(string title, string message, string messageUltraDetailed, bool throwGlobalError);
        Task AddWarning(string title, string message, string messageUltraDetailed);
        Task AddLogOnly(string title, string message, string messageUltraDetailed);
        Task AddSuccess(string title, string message, string messageUltraDetailed);
        Task AddInfo(string title, string message, string messageUltraDetailed);
    }

   
}
