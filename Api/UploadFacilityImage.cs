using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using BlazorApp.Api.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.ImageWrapper;

namespace BlazorApp.Api
{
    public class UploadFacilityImage
    {
        private readonly IBlobService _blobService;
        private readonly IImageProcessor _imageProcessor;

        public UploadFacilityImage(IBlobService blobService, IImageProcessor imageProcessor)
        {
            _blobService = blobService;
            _imageProcessor = imageProcessor;
        }

        [FunctionName("UploadFacilityImage")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var image = JsonConvert.DeserializeObject<ImageDto>(requestBody);

                var resizedImage = await _imageProcessor.CompressAsync(image.Content, 50);
                image.Content = resizedImage;
                var result = await _blobService.UploadImageAsync(image);

                return new OkObjectResult(result.AbsoluteUri);
            }
            catch (Exception e)
            {
                return new ExceptionResult(e, true);
            }
        }
    }
}
