using BlazorApp.Api.Entities;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Api.Extensions
{
    public static class AuditItemPhotoDtoExtensions
    {
        public static AuditItemPhoto CreateEntity(this AuditItemPhotoDto dto)
        {
            var entity = new AuditItemPhoto
            {
                Name = dto.Name,
                PreviewUrl = dto.PreviewUrl,
                Removed = dto.Removed
            };
            return entity;
        }
    }
}