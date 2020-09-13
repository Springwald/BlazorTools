using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;

namespace de.springwald.blazortools.Services
{
    public class TwaddleService
    {
        private readonly IToastService toastService;

        private int numberUnseenTwaddles = 0;
        private readonly List<TwaddleMessage> allTwaddles = new List<TwaddleMessage>();

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

        public event EventHandler<int> NumberUnseenTwaddlesChanged; 
        public event EventHandler<TwaddleMessage> NewTwaddle;

        public TwaddleService(IToastService toastService)
        {
            this.toastService = toastService;
            System.Console.Out.WriteLine("instanciated TwaddleService");
        }

        // Twaddle = Events - LogOnly;
        public TwaddleMessage[] GetAllNotifications()
        {
            return this.allTwaddles.Where(n => n.Typ != TwaddleTypes.LogOnly).ToArray();
        }

        // All Events including LogOnly
        public TwaddleMessage[] GetAllEvents()
        {
            return this.allTwaddles.ToArray();
        }

        public void SetAllNotificationsAsSeen()
        {
            this.allTwaddles.ForEach(n => { n.Seen = true; });
            this.numberUnseenTwaddles = 0;
        }

        public async Task AddError(string title, string message, string messageUltraDetailed, bool showInConsole = true)
        {
            if (showInConsole) System.Console.Error.WriteLine($"{title}: {message}");
            await this.AddNotification(title, message, messageUltraDetailed, TwaddleTypes.Error);
        }

        public async Task AddWarning(string title, string message, string messageUltraDetailed)
        {
            System.Console.Out.WriteLine($"WARN: {title}: {message}");
            await this.AddNotification(title, message, messageUltraDetailed, TwaddleTypes.Warning);
        }

        public async Task AddLogOnly(string title, string message, string messageUltraDetailed)
        {
            System.Console.Out.WriteLine($"{title}: {message}");
            await this.AddNotification(title, message, messageUltraDetailed, TwaddleTypes.LogOnly);
        }

        public async Task AddSuccess(string title, string message, string messageUltraDetailed)
        {
            await this.AddNotification(title, message, messageUltraDetailed, TwaddleTypes.Success);
        }

        public async Task AddInfo(string title, string message, string messageUltraDetailed)
        {
            System.Console.Out.WriteLine($"{title}: {message}");
            await this.AddNotification(title, message, messageUltraDetailed, TwaddleTypes.Info);
        }

        private async Task AddNotification(string title, string message, string messageUltraDetailed, TwaddleTypes type = TwaddleTypes.Info)
        {
            if (string.IsNullOrWhiteSpace(messageUltraDetailed))
            {
                messageUltraDetailed = message;
            }

            ToastLevel? level = null;
            switch (type)
            {
                case TwaddleTypes.LogOnly:
                    if (Debugger.IsAttached) level = ToastLevel.Info;
                    break;

                case TwaddleTypes.Error:
                    level = ToastLevel.Error;
                    break;

                case TwaddleTypes.Warning:
                    level = ToastLevel.Warning;
                    break;

                case TwaddleTypes.Success:
                    level = ToastLevel.Success;
                    break;

                case TwaddleTypes.Info:
                default:
                    level = ToastLevel.Info;
                    break;
            }

            var twaddle = new TwaddleMessage
            {
                UltraDetailedMessage = messageUltraDetailed,
                Message = message,
                Seen = false,
                Time = DateTime.Now,
                Title = title,
                Typ = type
            };

            // save notification
            this.allTwaddles.Add(twaddle);
            if (level.HasValue)
            {
                this.toastService.ShowToast(level.Value, message, title);
                this.numberUnseenTwaddles++;
            }

            // announce notification
            this.NewTwaddle?.Invoke(this, twaddle);
            this.NumberUnseenTwaddlesChanged?.Invoke(this, this.numberUnseenTwaddles);

            await Task.CompletedTask;
        }
    }
}