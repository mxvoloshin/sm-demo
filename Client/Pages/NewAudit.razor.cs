using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using BlazorApp.Client.Extensions;
using BlazorApp.Client.Models;
using BlazorApp.Client.Service;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Client.Pages
{
    public class NewAuditBase : ComponentBase
    {
        private CancellationTokenSource _cancellationTokenSource;
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

        protected async Task OnSubmitAsync()
        {
            try
            {
                //var newFacilityDto = new NewFacilityDto
                //{
                //    Name = Facility.Name,
                //    Address = Facility.Address
                //};

                //if (Facility.Picture != null)
                //{
                //    var imageDto = new ImageDto
                //    {
                //        Name = Facility.Picture.Name,
                //        ContentType = Facility.Picture.ContentType,
                //        Content = await Facility.Picture.GetResizedImageAsync(1000, _cancellationTokenSource.Token)
                //    };

                //    newFacilityDto.Image = imageDto;
                //}

                //var result = await HttpClient.PostAsJsonAsync("/api/NewFacility", newFacilityDto);
                //if (!result.IsSuccessStatusCode)
                //{

                //}
                //else
                //{


                //    NavigationManager.NavigateTo("dashboard");
                //}
            }
            catch (Exception e)
            {

            }

        }

        protected async Task OnFileSelected(InputFileChangeEventArgs eventArgs)
        {
            //Facility.Picture = eventArgs.File;
            //Facility.PreviewUrl = await Facility.Picture.GetPreviewUrlAsync(300, _cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}