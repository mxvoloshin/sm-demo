using Microsoft.Azure.Cosmos.Table;
using System;

namespace BlazorApp.Api.Entities
{
    public interface IAuditableEntity : ITableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
