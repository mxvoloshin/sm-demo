using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Models
{
    public class AuditItemGroupModel
    {
        public AuditItemGroupModel(IEnumerable<AuditItemModel> items)
        {
            Items = new List<AuditItemModel>(items);
        }

        public ushort Order { get; set; }
        public string Title { get; set; }
        public IEnumerable<AuditItemModel> Items { get; } = Enumerable.Empty<AuditItemModel>();
    }
}
