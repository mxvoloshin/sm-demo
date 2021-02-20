using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorApp.Shared.Audit;
using BlazorApp.Shared.Facility;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages
{
    public class AuditsBase : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }
        protected IEnumerable<AuditPreviewDto> Audits { get; set; }

        [Parameter]
        public string FacilityId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Audits = await HttpClient.GetFromJsonAsync<IEnumerable<AuditPreviewDto>>($"api/audits?facilityId={FacilityId}");
        }
    }
}
