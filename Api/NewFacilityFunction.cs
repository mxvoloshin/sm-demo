using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using BlazorApp.Api.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BlazorApp.Api.Repository;
using BlazorApp.Shared;
using BlazorApp.Api.Entities;
using BlazorApp.Api.Services;
using BlazorApp.Shared.Facility;
using Shared.ImageWrapper;

namespace BlazorApp.Api
{
    public class NewFacilityFunction
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IBlobService _blobService;
        private readonly IImageProcessor _imageProcessor;

        public NewFacilityFunction(IFacilityRepository facilityRepository, IDateTimeService dateTimeService, IBlobService blobService, IImageProcessor imageProcessor)
        {
            _facilityRepository = facilityRepository;
            _dateTimeService = dateTimeService;
            _blobService = blobService;
            _imageProcessor = imageProcessor;
        }

        [FunctionName("NewFacility")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                var claims = req.GetClaimsPrincipal();

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var facility = JsonConvert.DeserializeObject<NewFacilityDto>(requestBody);

                var entity = new Facility
                {
                    PartitionKey = $"{DateTime.Now.Year}{DateTime.Now.Month}",
                    RowKey = Guid.NewGuid().ToString(),
                    Timestamp = _dateTimeService.TableEntityTimeStamp,
                    Name = facility.Name,
                    Address = facility.Address,
                    CreatedBy = claims.ClientId(),
                    CreatedAt = _dateTimeService.CurrentUtcDateTime
                };

                if (facility.Image != null)
                {
                    var resizedImage = await _imageProcessor.CompressAsync(facility.Image.Content, 50);
                    facility.Image.Content = resizedImage;
                    var imageUri = await _blobService.UploadImageAsync(facility.Image);
                    entity.PreviewUrl = imageUri.AbsoluteUri;
                }

                var result = await _facilityRepository.CreateAsync(entity);

                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                return new ExceptionResult(e, true);
            }
        }
    }
}
