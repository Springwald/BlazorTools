// A small collection of everyday components and services for dotnet Blazor
// https://github.com/Springwald/BlazorTools
//
// (C) 2021 Daniel Springwald, Bochum Germany
// Springwald Software  -   www.springwald.de
// daniel@springwald.de -  +49 234 298 788 46
// All rights reserved
// Licensed under MIT License

using Microsoft.AspNetCore.Components;

namespace de.springwald.blazortools.Components
{
    public partial class ConfirmButton : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string ButtonText { get; set; }

        [Parameter]
        public string OkButtonText { get; set; } = "OK";

        [Parameter]
        public string CancelButtonText { get; set; } = "Cancel";

        [Parameter]
        public string ConfirmHeader { get; set; }

        [Parameter]
        public string ButtonClass { get; set; }

        [Parameter]
        public string IconClass { get; set; }

        [Parameter]
        public string ConfirmQuestion { get; set; }

        [Parameter] public EventCallback OnClickOk { get; set; }
        [Parameter] public EventCallback OnClickCancel { get; set; }


        public void Hide()
        {
            this.hideModalDialog();
        }

        private bool dialogVisible;
        private string modalClass;

        private void buttonClicked()
        {
            this.showModalDialog();
        }
        private void showModalDialog()
        {
            this.dialogVisible = true;
            this.modalClass = "show";
            StateHasChanged();
        }

        private void hideModalDialog()
        {
            this.dialogVisible = false;
            this.modalClass = "";
            StateHasChanged();
        }

    }
}
