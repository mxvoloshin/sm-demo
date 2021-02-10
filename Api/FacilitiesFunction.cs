using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Api.Repository;
using BlazorApp.Api.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlazorApp.Api
{
    public class FacilitiesFunction
    {
        private readonly IFacilityRepository _facilityRepository;
        
        public FacilitiesFunction(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        [FunctionName("Facilities")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            var facilities = _facilityRepository.Find(x=>x.RowKey != string.Empty).ToList();
            return new OkObjectResult(facilities.Select(x=> new FacilityPreviewDto
            {
                Id = x.RowKey,
                Name = x.Name,
                Address = x.Address,
                PreviewUrl = x.PreviewUrl
            }));
        }
    }
}
