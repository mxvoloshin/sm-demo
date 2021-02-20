using System.Collections.Generic;

namespace BlazorApp.Api.Entities
{
    public class AuditItemGroup
    {
        public ushort Order { get; set; }
        public string Title { get; set; }
        public IList<AuditItem> Items { get; } = new List<AuditItem>();
    }
}