using System.Linq;
using BlazorApp.Client.Models;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Client.Extensions
{
    public static class AuditDtoExtensions
    {
        public static AuditModel CreateModel(this AuditDto dto)
        {
            var groups = dto.Groups.Select(x => x.CreateModel()).ToList();

            var model = new AuditModel(groups)
            {
                Id = dto.Id,
                FacilityId = dto.FacilityId,
                StartTime = dto.StartTimeUtc.ToLocalTime(),
                FinishTime = dto.FinishTimeUtc.ToLocalTime()
            };
            return model;
        }
    }
}