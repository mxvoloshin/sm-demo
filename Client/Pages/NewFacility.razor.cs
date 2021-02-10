using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Client.Extensions;
using BlazorApp.Client.Models;
using BlazorApp.Shared;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Client.Pages
{
    public class NewFacilityBase : ComponentBase, IDisposable
    {
        [Inject]
        protected HttpClient HttpClient { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        protected FacilityModel Facility = new FacilityModel();

        private CancellationTokenSource _cancellationTokenSource;

        protected override void OnInitialized()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected async Task HandleValidSubmit()
        {
            var newFacilityDto = new NewFacilityDto
            {
                Name = Facility.Name,
                Address = Facility.Address
            };

            if (Facility.Picture != null)
            {
                var imageDto = new ImageDto
                {
                    Name = Facility.Picture.Name,
                    ContentType = Facility.Picture.ContentType,
                    Content = await Facility.Picture.GetResizedImageAsync(1000, _cancellationTokenSource.Token)
                };

                newFacilityDto.Image = imageDto;
            }

            var result = await HttpClient.PostAsJsonAsync("/api/NewFacility", newFacilityDto);
            if (!result.IsSuccessStatusCode)
            {
                Toaster.Add("Ошибка при сохранении", MatToastType.Danger);
            }
            else
            {
                Toaster.Add("Объект сохранен", MatToastType.Success);
                NavigationManager.NavigateTo("dashboard");
            }
        }

        protected async Task OnFileSelected(InputFileChangeEventArgs eventArgs)
        {
            Facility.Picture = eventArgs.File;
            Facility.PreviewUrl = await Facility.Picture.GetPreviewUrlAsync(300, _cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}
