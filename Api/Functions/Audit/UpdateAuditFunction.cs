using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BlazorApp.Api.Auth;
using BlazorApp.Api.Extensions;
using BlazorApp.Api.Repository;
using BlazorApp.Api.Services;
using BlazorApp.Shared.Audit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlazorApp.Api.Functions.Audit
{
    public class UpdateAuditFunction : AuditFunctionBase
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IDateTimeService _dateTimeService;

        public UpdateAuditFunction(IAuditRepository auditRepository,
            IFacilityRepository facilityRepository,
            IBlobService blobService,
            IDateTimeService dateTimeService) : base(blobService)
        {
            _auditRepository = auditRepository;
            _facilityRepository = facilityRepository;
            _dateTimeService = dateTimeService;
        }
        
        [FunctionName("UpdateAudit")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function,
                "put",
                Route = "facilities/{facilityId}/audits/{auditId}")]
            HttpRequest req,
            ILogger log, string facilityId, string auditId)
        {
            try
            {
                var facility = _facilityRepository.FindById(facilityId);
                if (facility == null)
                {
                    return new NotFoundResult();
                }

                var audit = _auditRepository.FindById(auditId);
                if (audit == null)
                {
                    return new NotFoundResult();
                }

                var claims = req.GetClaimsPrincipal();

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var auditDto = JsonConvert.DeserializeObject<AuditDto>(requestBody);

                await ProcessPhotosAsync(auditDto.Groups);
                var groups = auditDto.Groups.Select(x => x.CreateEntity()).ToList();

                audit.StartTimeUtc = auditDto.StartTimeUtc;
                audit.FinishTimeUtc = auditDto.FinishTimeUtc;
                audit.GroupsJson = JsonConvert.SerializeObject(groups);
                audit.ModifiedBy = claims.ClientId();
                audit.ModifiedAt = _dateTimeService.CurrentUtcDateTime;

                var result = await _auditRepository.UpdateAsync(audit);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                return new ExceptionResult(e, true);
            }
        }

    }
}