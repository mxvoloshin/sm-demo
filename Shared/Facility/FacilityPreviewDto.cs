﻿namespace BlazorApp.Shared.Facility
{
    public class FacilityPreviewDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        private string _previewUrl;
        public string PreviewUrl
        {
            get => string.IsNullOrEmpty(_previewUrl) ? @"empty-image.png" : _previewUrl;
            set => _previewUrl = value;
        }
    }
}
