﻿using Blazored.Toast.Configuration;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace de.springwald.blazortools.Components.toast
{

    public enum IconType { FontAwesome, Material };

    public partial class BlazoredToasts: ComponentBase
    {
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        [Parameter] public IconType? IconType { get; set; }
        [Parameter] public string InfoClass { get; set; }
        [Parameter] public string InfoIcon { get; set; }
        [Parameter] public string SuccessClass { get; set; }
        [Parameter] public string SuccessIcon { get; set; }
        [Parameter] public string WarningClass { get; set; }
        [Parameter] public string WarningIcon { get; set; }
        [Parameter] public string ErrorClass { get; set; }
        [Parameter] public string ErrorIcon { get; set; }
        [Parameter] public ToastPosition Position { get; set; } = ToastPosition.TopRight;
        [Parameter] public int Timeout { get; set; } = 5;
        [Parameter] public int ErrorTimeout { get; set; } = 20;
        [Parameter] public int WarningTimeout { get; set; } = 10;
        [Parameter] public bool RemoveToastsOnNavigation { get; set; }
        [Parameter] public bool ShowProgressBar { get; set; }

        private string PositionClass { get; set; } = string.Empty;
        internal List<ToastInstance> ToastList { get; set; } = new List<ToastInstance>();

        protected override void OnInitialized()
        {
            ToastService.OnShow += ShowToast;

            if (RemoveToastsOnNavigation)
            {
                NavigationManager.LocationChanged += ClearToasts;
            }

            PositionClass = $"position-{Position.ToString().ToLower()}";

            if ((!string.IsNullOrEmpty(InfoIcon)
                 || !string.IsNullOrEmpty(SuccessIcon)
                 || !string.IsNullOrEmpty(WarningIcon)
                 || !string.IsNullOrEmpty(ErrorIcon)
                ) && IconType == null)
            {
                throw new ArgumentException("If an icon is specified then IconType is a required parameter.");
            }
        }

        public void RemoveToast(Guid toastId)
        {
            InvokeAsync(() =>
            {
                var toastInstance = ToastList.SingleOrDefault(x => x.Id == toastId);
                ToastList.Remove(toastInstance);
                StateHasChanged();
            });
        }

        private void ClearToasts(object sender, LocationChangedEventArgs args)
        {
            InvokeAsync(() =>
            {
                ToastList.Clear();
                StateHasChanged();
            });
        }

        private ToastSettings BuildToastSettings(ToastLevel level, RenderFragment message, string heading)
        {
            switch (level)
            {
                case ToastLevel.Error:
                    return new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Error" : heading, message, IconType, "blazored-toast-error", ErrorClass, ErrorIcon, ShowProgressBar, timeout: ErrorTimeout);

                case ToastLevel.Info:
                    return new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Info" : heading, message, IconType, "blazored-toast-info", InfoClass, InfoIcon, ShowProgressBar, timeout: Timeout);

                case ToastLevel.Success:
                    return new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Success" : heading, message, IconType, "blazored-toast-success", SuccessClass, SuccessIcon, ShowProgressBar, timeout: Timeout);

                case ToastLevel.Warning:
                    return new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Warning" : heading, message, IconType, "blazored-toast-warning", WarningClass, WarningIcon, ShowProgressBar, timeout: WarningTimeout);
            }
            throw new ArgumentOutOfRangeException($"{nameof(level)}:{level}");
        }

        private void ShowToast(ToastLevel level, RenderFragment message, string heading)
        {
            InvokeAsync(() =>
            {
                var settings = BuildToastSettings(level, message, heading);
                var toast = new ToastInstance
                {
                    Id = Guid.NewGuid(),
                    TimeStamp = DateTime.Now,
                    ToastSettings = settings,
                };

                ToastList.Add(toast);

                StateHasChanged();
            });

        }
    }
}
