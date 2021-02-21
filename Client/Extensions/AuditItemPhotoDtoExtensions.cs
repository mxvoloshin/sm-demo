using BlazorApp.Client.Models;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Client.Extensions
{
    public static class AuditItemPhotoDtoExtensions
    {
        public static AuditItemPhotoModel CreateModel(this AuditItemPhotoDto dto)
        {
            var model = new AuditItemPhotoModel
            {
                Name = dto.Name,
                PreviewUrl = dto.PreviewUrl,
                Removed = dto.Removed
            };
            return model;
        }
    }
}