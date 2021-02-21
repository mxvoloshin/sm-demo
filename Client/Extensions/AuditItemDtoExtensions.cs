using System.Linq;
using BlazorApp.Client.Models;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Client.Extensions
{
    public static class AuditItemDtoExtensions
    {
        public static AuditItemModel CreateModel(this AuditItemDto dto)
        {
            var model = new AuditItemModel
            {
                Order = dto.Order,
                Title = dto.Title,
                IsCheckedAvailable = dto.IsCheckedAvailable,
                IsChecked = dto.IsChecked,
                IsCommentsAvailable = dto.IsCommentsAvailable,
                Comments = dto.Comments,
                IsPhotoAvailable = dto.IsPhotoAvailable,
                Photos = dto.Photos.Select(x => x.CreateModel()).ToList()
            };
            return model;
        }
    }
}