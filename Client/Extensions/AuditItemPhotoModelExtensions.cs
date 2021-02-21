using BlazorApp.Client.Models;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Client.Extensions
{
    public static class AuditItemPhotoModelExtensions
    {
        public static AuditItemPhotoDto CreateDto(this AuditItemPhotoModel model)
        {
            var dto = new AuditItemPhotoDto
            {
                Name = model.Name,
                Content = model.Content,
                ContentType = model.ContentType,
                PreviewUrl = model.PreviewUrl,
                Removed = model.Removed
            };
            return dto;
        }
    }
}