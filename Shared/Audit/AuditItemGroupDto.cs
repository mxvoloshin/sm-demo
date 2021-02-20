using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Shared.Audit
{
    public class AuditItemGroupDto
    {
        public ushort Order { get; set; }
        public string Title { get; set; }
        public IList<AuditItemDto> Items { get; set; } = new List<AuditItemDto>();
    }
}