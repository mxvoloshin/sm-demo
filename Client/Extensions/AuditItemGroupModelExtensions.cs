using System.Linq;
using BlazorApp.Client.Models;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Client.Extensions
{
    public static class AuditItemGroupModelExtensions
    {
        public static AuditItemGroupDto CreateDto(this AuditItemGroupModel model)
        {
            var dto = new AuditItemGroupDto
            {
                Order = model.Order,
                Title = model.Title,
                Items = model.Items.Select(x => x.CreateDto()).ToList()
            };
            return dto;
        }
    }
}