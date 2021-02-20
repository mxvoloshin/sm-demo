using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Api.Repository;
using BlazorApp.Shared.Audit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Api.Functions.Audit
{
    public class AuditsFunction
    {
        private readonly IAuditRepository _auditRepository;

        public AuditsFunction(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        [FunctionName("Audits")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            var facilityId = req.Query["facilityId"].ToString();
            if (string.IsNullOrEmpty(facilityId))
            {
                return new BadRequestResult();
            }

            var audits = _auditRepository.Find(x => x.FacilityId == facilityId).ToList();
            return new OkObjectResult(audits.Select(x => new AuditPreviewDto
            {
                Id = x.RowKey,
                FacilityId = x.FacilityId,
                StartTimeUtc = x.StartTimeUtc,
                FinishTimeUtc = x.FinishTimeUtc
            }));
        }
    }
}