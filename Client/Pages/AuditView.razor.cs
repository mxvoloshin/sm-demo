using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorApp.Shared.Audit;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages
{
    public class AuditViewBase : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        protected AuditViewDto Audit { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Audit = await HttpClient.GetFromJsonAsync<AuditViewDto>($"api/audits/{Id}");
        }
    }
}
