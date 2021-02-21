using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BlazorApp.Api.Auth;
using BlazorApp.Api.Entities;
using BlazorApp.Api.Extensions;
using BlazorApp.Api.Repository;
using BlazorApp.Api.Services;
using BlazorApp.Shared;
using BlazorApp.Shared.Audit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.ImageWrapper;

namespace BlazorApp.Api.Functions.Audit
{
    public class CreateAuditFunction : AuditFunctionBase
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IDateTimeService _dateTimeService;
        
        public CreateAuditFunction(IAuditRepository auditRepository, 
            IFacilityRepository facilityRepository,
            IBlobService blobService,
            IDateTimeService dateTimeService) : base(blobService)
        {
            _auditRepository = auditRepository;
            _facilityRepository = facilityRepository;
            _dateTimeService = dateTimeService;
        }

        [FunctionName("CreateAudit")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function,
                "post",
                Route = "facilities/{facilityId}/audits")]
            HttpRequest req,
            ILogger log, string facilityId)
        {
            try
            {
                var facility = _facilityRepository.FindById(facilityId);
                if (facility == null)
                {
                    return new NotFoundResult();
                }

                var claims = req.GetClaimsPrincipal();

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var audit = JsonConvert.DeserializeObject<AuditDto>(requestBody);

                await ProcessPhotosAsync(audit.Groups);
                var groups = audit.Groups.Select(x => x.CreateEntity()).ToList();

                var entity = new Entities.Audit
                {
                    PartitionKey = $"{DateTime.Now.Year}{DateTime.Now.Month}",
                    RowKey = Guid.NewGuid().ToString(),
                    Timestamp = _dateTimeService.TableEntityTimeStamp,
                    FacilityId = facilityId,
                    StartTimeUtc = audit.StartTimeUtc,
                    FinishTimeUtc = audit.FinishTimeUtc,
                    GroupsJson = JsonConvert.SerializeObject(groups),
                    CreatedBy = claims.ClientId(),
                    CreatedAt = _dateTimeService.CurrentUtcDateTime
                };

                var result = await _auditRepository.CreateAsync(entity);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                return new ExceptionResult(e, true);
            }
        }
    }
}