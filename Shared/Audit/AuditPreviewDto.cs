using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.Audit
{
    public class AuditPreviewDto
    {
        public string Id { get; set; }
        public string FacilityId { get; set; }
        public DateTimeOffset StartTimeUtc { get; set; }
        public DateTimeOffset FinishTimeUtc { get; set; }
    }
}
