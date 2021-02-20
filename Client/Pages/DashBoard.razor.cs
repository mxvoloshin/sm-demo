using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorApp.Shared;
using BlazorApp.Shared.Facility;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages
{
    public class DashBoardBase : ComponentBase
    {
        [Inject] 
        public HttpClient HttpClient { get; set; }

        protected string Filter { get; set; }
        protected IEnumerable<FacilityPreviewDto> Facilities { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            Facilities = await GetFacilitiesAsync(Filter);
        }

        private async Task<IEnumerable<FacilityPreviewDto>> GetFacilitiesAsync(string filter)
        {
            var previews = await HttpClient.GetFromJsonAsync<IEnumerable<FacilityPreviewDto>>("api/facilities");
            return previews;
        }
    }
}
