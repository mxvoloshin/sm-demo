using System.Collections.Generic;

namespace BlazorApp.Api.Entities
{
    public class AuditItem
    {
        public ushort Order { get; set; }
        public string Title { get; set; }
        public bool IsCheckedAvailable { get; set; }
        public bool IsChecked { get; set; }
        public bool IsCommentsAvailable { get; set; }
        public string Comments { get; set; }
        public bool IsPhotoAvailable { get; set; }
        public IList<AuditItemPhoto> Photos { get; set; } = new List<AuditItemPhoto>();
    }
}