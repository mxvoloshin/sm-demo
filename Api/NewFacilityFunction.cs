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

namespace BlazorApp.Api
{
    public class NewFacilityFunction
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly IDateTimeService _dateTimeService;

        public NewFacilityFunction(IFacilityRepository facilityRepository, IDateTimeService dateTimeService)
        {
            _facilityRepository = facilityRepository;
            _dateTimeService = dateTimeService;
        }

        [FunctionName("NewFacility")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                var claims = req.GetClaimsPrincipal();

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var facility = JsonConvert.DeserializeObject<FacilityDto>(requestBody);

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
