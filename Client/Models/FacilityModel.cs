using System.ComponentModel.DataAnnotations;
using BlazorApp.Client.Attributes;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Client.Models
{
    public class FacilityModel
    {
        private string _previewUrl;
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [FileValidation(new[] { ".png", ".jpg" })]
        public IBrowserFile Picture { get; set; }

        public string PreviewUrl
        {
            get => string.IsNullOrEmpty(_previewUrl) ? @"empty-image.png" : _previewUrl;
            set => _previewUrl = value;
        }
    }
}
