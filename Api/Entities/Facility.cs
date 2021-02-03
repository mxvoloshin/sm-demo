using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Api.Entities
{
    public class Facility : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
