using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.Audit
{
    public class AuditItemPhotoDto
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public string PreviewUrl { get; set; }
        public bool Removed { get; set; }
    }
}
