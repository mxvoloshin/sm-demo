using System;
using System.Collections.Generic;

namespace BlazorApp.Api.Entities
{
    public class Audit : BaseEntity
    {
        public string FacilityId { get; set; }
        public DateTimeOffset StartTimeUtc { get; set; }
        public DateTimeOffset FinishTimeUtc { get; set; }
        public string GroupsJson { get; set; }
    }
}