using System.Linq;
using BlazorApp.Client.Models;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Client.Extensions
{
    public static class AuditModelExtensions
    {
        public static AuditDto CreateDto(this AuditModel model)
        {
            var dto = new AuditDto
            {
                Id = model.Id,
                FacilityId = model.FacilityId,
                StartTimeUtc = model.StartTime.UtcDateTime,
                FinishTimeUtc = model.FinishTime.UtcDateTime,
                Groups = model.Groups.Select(x=>x.CreateDto()).ToList()
            };
            return dto;
        }
    }
}