using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Models
{
    public class AuditItemModel
    {
        public ushort Order { get; set; }
        public bool IsCheckedAvailable { get; set; }
        public bool IsChecked { get; set; }
        public string Title { get; set; }
        public bool IsCommentsAvailable { get; set; }
        public string Comments { get; set; }
    }
}
