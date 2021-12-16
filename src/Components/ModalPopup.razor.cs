// A small collection of everyday components and services for dotnet Blazor
// https://github.com/Springwald/BlazorTools
//
// (C) 2021 Daniel Springwald, Bochum Germany
// Springwald Software  -   www.springwald.de
// daniel@springwald.de -  +49 234 298 788 46
// All rights reserved
// Licensed under MIT License

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace de.springwald.blazortools.Components
{
    public partial class ModalPopup : ComponentBase
    {
        [Parameter]
        public RenderFragment Header { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment Footer { get; set; }

        [Parameter]
        public bool Xl { get; set; }

        [Parameter]
        public bool DialogueVisible { get; set; }

        private string modalClass;

        public async Task Show()
        {
            this.DialogueVisible = true;
            this.modalClass = "show";
            StateHasChanged();
            await Task.CompletedTask;
        }

        public async Task Hide()
        {
            this.DialogueVisible = false;
            this.modalClass = "";
            StateHasChanged();
            await Task.CompletedTask;
        }
    }
}
