using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Api.Entities;
using BlazorApp.Api.Repository;
using BlazorApp.Shared.Audit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlazorApp.Api.Functions.Audit
{
    public class GetAuditFunction
    {
        private readonly IAuditRepository _auditRepository;

        public GetAuditFunction(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        [FunctionName("GetAudit")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "audits/{id}")]
            HttpRequest req, ILogger log, string id)
        {
            var audit = _auditRepository.FindById(id);
            if (audit == null)
            {
                return new NotFoundResult();
            }

            var auditViewDto = new AuditViewDto
            {
                Id = audit.RowKey,
                FacilityId = audit.FacilityId,
                StartTimeUtc = audit.StartTimeUtc,
                FinishTimeUtc = audit.FinishTimeUtc,
                Groups = JsonConvert.DeserializeObject<IList<AuditItemGroupDto>>(audit.GroupsJson)
            };

            return new OkObjectResult(auditViewDto);
        }
    }
}