using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Client.Models;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Client.Extensions
{
    public static class AuditItemModelExtensions
    {
        public static AuditItemDto CreateDto(this AuditItemModel model)
        {
            var dto = new AuditItemDto
            {
                Order = model.Order,
                Title = model.Title,
                IsCheckedAvailable = model.IsCheckedAvailable,
                IsChecked = model.IsChecked,
                IsCommentsAvailable = model.IsCommentsAvailable,
                Comments = model.Comments,
                IsPhotoAvailable = model.IsPhotoAvailable,
                Photos = model.Photos.Select(x => x.CreateDto()).ToList()
            };
            
            return dto;
        }

        public static AuditItemModel WithComments(this AuditItemModel model)
        {
            model.IsCommentsAvailable = true;
            return model;
        }
        public static AuditItemModel WithAttachments(this AuditItemModel model)
        {
            model.IsPhotoAvailable = true;
            return model;
        }
    }
}
