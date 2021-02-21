using System.Linq;
using BlazorApp.Api.Entities;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Api.Extensions
{
    public static class AuditItemGroupDtoExtensions
    {
        public static AuditItemGroup CreateEntity(this AuditItemGroupDto dto)
        {
            var entity = new AuditItemGroup
            {
                Order = dto.Order,
                Title = dto.Title,
                Items = dto.Items.Select(x => x.CreateEntity()).ToList()
            };
            return entity;
        }
    }
}