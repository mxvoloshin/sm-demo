using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Shared.Audit
{
    public class NewAuditDto
    {
        public string FacilityId { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime FinishTimeUtc { get; set; }
        public IList<AuditItemGroupDto> Groups { get; set; } = new List<AuditItemGroupDto>();
    }
}