using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Components
{
    public class FacilityCardBase : ComponentBase
    {
        [Parameter]
        public FacilityPreviewDto Model { get; set; }
    }
}
