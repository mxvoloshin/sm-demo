using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Shared.Audit
{
    public class AuditDto
    {
        public string Id { get; set; }
        public string FacilityId { get; set; }
        public DateTimeOffset StartTimeUtc { get; set; }
        public DateTimeOffset FinishTimeUtc { get; set; }
        public IList<AuditItemGroupDto> Groups { get; set; } = new List<AuditItemGroupDto>();
    }
}