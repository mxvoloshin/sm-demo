using System.Linq;
using BlazorApp.Api.Entities;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Api.Extensions
{
    public static class AuditItemDtoExtensions
    {
        public static AuditItem CreateEntity(this AuditItemDto dto)
        {
            var entity = new AuditItem
            {
                Order = dto.Order,
                Title = dto.Title,
                IsCheckedAvailable = dto.IsCheckedAvailable,
                IsChecked = dto.IsChecked,
                IsCommentsAvailable = dto.IsCommentsAvailable,
                Comments = dto.Comments,
                IsPhotoAvailable = dto.IsPhotoAvailable,
                Photos = dto.Photos.Where(x => !x.Removed).Select(x => x.CreateEntity()).ToList()
            };

            return entity;
        }
    }
}