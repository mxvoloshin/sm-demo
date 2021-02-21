using System.Linq;
using BlazorApp.Client.Models;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Client.Extensions
{
    public static class AuditItemGroupDtoExtensions
    {
        public static AuditItemGroupModel CreateModel(this AuditItemGroupDto dto)
        {
            var items = dto.Items.Select(x => x.CreateModel()).ToList();
            var model = new AuditItemGroupModel(items)
            {
                Order = dto.Order,
                Title = dto.Title
            };
            return model;
        }
    }
}