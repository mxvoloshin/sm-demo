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
    public class NewAuditBase : ComponentBase
    {
        private CancellationTokenSource _cancellationTokenSource;
        protected CustomValidator customValidator;
        protected AuditModel Audit = new AuditModel(AuditItemFactory.CreateDefaultAuditGroups());

        [Inject]
        protected HttpClient HttpClient { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string FacilityId { get; set; }

        protected override void OnInitialized()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Audit.FacilityId = FacilityId;
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
                
                var newAuditDto = await BuildNewAuditDtoFromModelAsync(Audit);

                var result = await HttpClient.PostAsJsonAsync("/api/NewAudit", newAuditDto);
                if (!result.IsSuccessStatusCode)
                {

                }
                else
                {
                    NavigationManager.NavigateTo("dashboard");
                }
            }
            catch (Exception e)
            {

            }

        }

        protected async Task OnFileSelected(InputFileChangeEventArgs eventArgs, AuditItemModel auditItem)
        {
            auditItem.Photos.Clear();
            foreach (var file in eventArgs.GetMultipleFiles())
            {
                auditItem.Photos.Add(new AuditItemPhotoModel
                {
                    File = file,
                    PreviewUrl = await file.GetPreviewUrlAsync(300, _cancellationTokenSource.Token)
                });
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }

        private async Task<NewAuditDto> BuildNewAuditDtoFromModelAsync(AuditModel model)
        {
            var newAuditDto = new NewAuditDto
            {
                FacilityId = model.FacilityId,
                StartTimeUtc = model.StartTime.UtcDateTime,
                FinishTimeUtc = model.FinishTime.UtcDateTime
            };

            foreach (var auditItemGroupModel in model.Groups)
            {
                var newAuditGroupDto = new AuditItemGroupDto
                {
                    Order = auditItemGroupModel.Order,
                    Title = auditItemGroupModel.Title
                };

                foreach (var auditItemModel in auditItemGroupModel.Items)
                {
                    var auditItemDto = new AuditItemDto
                    {
                        Title = auditItemModel.Title,
                        Order = auditItemModel.Order,
                        IsCheckedAvailable = auditItemModel.IsCheckedAvailable,
                        IsChecked = auditItemModel.IsChecked,
                        IsCommentsAvailable = auditItemModel.IsCommentsAvailable,
                        Comments = auditItemModel.Comments,
                        IsPhotoAvailable = auditItemModel.IsPhotoAvailable
                    };

                    foreach (var auditItemPhotoModel in auditItemModel.Photos)
                    {
                        var imageDto = new ImageDto
                        {
                            Name = $"audit_{Guid.NewGuid()}",
                            ContentType = auditItemPhotoModel.File.ContentType,
                            Content = await auditItemPhotoModel.File.GetResizedImageAsync(1000, _cancellationTokenSource.Token)
                        };

                        auditItemDto.Photos.Add(imageDto);
                    }

                    newAuditGroupDto.Items.Add(auditItemDto);
                }
                newAuditDto.Groups.Add(newAuditGroupDto);
            }

            return newAuditDto;
        }
    }
}