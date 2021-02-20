using System;
using System.Collections.Generic;

namespace BlazorApp.Api.Entities
{
    public class Audit : BaseEntity
    {
        public string FacilityId { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime FinishTimeUtc { get; set; }
        public string GroupsJson { get; set; }
    }
}