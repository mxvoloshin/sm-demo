using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Client.Models;

namespace BlazorApp.Client.Extensions
{
    public static class AuditItemModelExtensions
    {
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
