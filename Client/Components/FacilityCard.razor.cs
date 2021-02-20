using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Shared;
using BlazorApp.Shared.Facility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorApp.Client.Components
{
    public class FacilityCardBase : ComponentBase
    {
        [Parameter]
        public FacilityPreviewDto Model { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        public void OnNewAudit(MouseEventArgs e)
        {
            NavigationManager.NavigateTo($"facility/{Model.Id}/audit");
        }
    }
}
