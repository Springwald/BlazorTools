﻿using Microsoft.AspNetCore.Components;
using de.springwald.blazortools.Components.toast;

namespace Blazored.Toast.Configuration
{
    public class ToastSettings
    {
        public ToastSettings(
            string heading,
            RenderFragment message,
            IconType? iconType,
            string baseClass,
            string additionalClasses,
            string icon,
            bool showProgressBar,
            int timeout)
        {
            Heading = heading;
            Message = message;
            IconType = iconType;
            BaseClass = baseClass;
            AdditionalClasses = additionalClasses;
            Icon = icon;
            ShowProgressBar = showProgressBar;
            TimeOut = timeout;
        }

        public string Heading { get; set; }
        public RenderFragment Message { get; set; }
        public string BaseClass { get; set; }
        public string AdditionalClasses { get; set; }
        public string Icon { get; set; }
        public IconType? IconType { get; set; }
        public bool ShowProgressBar { get; set; }
        public int TimeOut { get; }
    }
}
