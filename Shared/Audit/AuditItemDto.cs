using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Shared.Audit
{
    public class AuditItemDto
    {
        public ushort Order { get; set; }
        public bool IsCheckedAvailable { get; set; }
        public bool IsChecked { get; set; }
        public string Title { get; set; }
        public bool IsCommentsAvailable { get; set; }
        public string Comments { get; set; }
        public bool IsPhotoAvailable { get; set; }
        public IList<ImageDto> Photos { get; set; } = new List<ImageDto>();
        public IList<string> PhotosPreview { get; set; } = new List<string>();
    }
}