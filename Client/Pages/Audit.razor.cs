using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Client.Components;
using BlazorApp.Client.Extensions;
using BlazorApp.Client.Models;
using BlazorApp.Client.Service;
using BlazorApp.Shared;
using BlazorApp.Shared.Audit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Client.Pages
{
    public class AuditBase : ComponentBase
    {
        private CancellationTokenSource _cancellationTokenSource;
        protected CustomValidator customValidator;
        protected AuditModel Audit;

        [Inject]
        protected HttpClient HttpClient { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string FacilityId { get; set; }

        [Parameter]
        public string AuditId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            if (string.IsNullOrEmpty(AuditId))
            {
                Audit = new AuditModel(AuditItemFactory.CreateDefaultAuditGroups()) {FacilityId = FacilityId};
            }
            else
            {
                var auditDto = await HttpClient.GetFromJsonAsync<AuditDto>($"api/audits/{AuditId}");
                Audit = auditDto.CreateModel();
            }
        }

        protected override void OnInitialized()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected async Task OnSubmitAsync()
        {
            try
            {
                customValidator.ClearErrors();

                var errors = new Dictionary<string, List<string>>();

                if (Audit.FinishTime < Audit.StartTime)
                {
                    errors.Add(nameof(Audit.FinishTime),
                        new List<string>() { "Время окончания аудита не может быть раньше времени начала" });
                }

                if (errors.Any())
                {
                    customValidator.DisplayErrors(errors);
                    return;
                }

                var auditDto = Audit.CreateDto();

                var result = await CreateOrUpdateAsync(auditDto);
                if (!result.IsSuccessStatusCode)
                {

                }

                NavigationManager.NavigateTo($"/facilities/{FacilityId}/audits");
            }
            catch (Exception e)
            {

            }

        }

        protected async Task OnFileSelected(InputFileChangeEventArgs eventArgs, AuditItemModel auditItem)
        {
            foreach (var file in eventArgs.GetMultipleFiles())
            {
                auditItem.Photos.Add(new AuditItemPhotoModel
                {
                    Name = $"audit_{Guid.NewGuid()}",
                    File = file,
                    ContentType = file.ContentType,
                    Content = await file.GetResizedImageAsync(1000, _cancellationTokenSource.Token),
                    PreviewUrl = await file.GetPreviewUrlAsync(300, _cancellationTokenSource.Token)
                });
            }
        }

        protected void OnImageRemove(AuditItemPhotoModel model)
        {
            if (model.File == null)
            {
                model.Removed = true;
                return;
            }

            foreach (var auditItemGroupModel in Audit.Groups)
            {
                foreach (var auditItemModel in auditItemGroupModel.Items)
                {
                    if (auditItemModel.Photos.All(x => x.Name != model.Name))
                    {
                        continue;
                    }

                    auditItemModel.Photos.Remove(model);
                    return;
                }
            }
        }

        protected async Task<HttpResponseMessage> CreateOrUpdateAsync(AuditDto auditDto)
        {
            if (string.IsNullOrEmpty(auditDto.Id))
            {
                return await HttpClient.PostAsJsonAsync($"api/facilities/{FacilityId}/audits", auditDto);
            }

            return await HttpClient.PutAsJsonAsync($"api/facilities/{FacilityId}/audits/{auditDto.Id}", auditDto);

        }
        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}