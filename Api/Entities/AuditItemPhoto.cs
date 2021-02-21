using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Api.Entities
{
    public class AuditItemPhoto
    {
        public string Name { get; set; }
        public string PreviewUrl { get; set; }
        public bool Removed { get; set; }
    }
}
