using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Client.Models
{
    public class AuditItemPhotoModel
    {
        public IBrowserFile File { get; set; }
        public string PreviewUrl { get; set; }
    }
}
