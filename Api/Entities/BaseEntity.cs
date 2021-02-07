using System;
using Microsoft.Azure.Cosmos.Table;

namespace BlazorApp.Api.Entities
{
    public abstract class BaseEntity : TableEntity, IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
