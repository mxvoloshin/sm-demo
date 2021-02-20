using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Models
{
    public class AuditModel
    {
        public AuditModel(IEnumerable<AuditItemGroupModel> groups)
        {
            Groups = new List<AuditItemGroupModel>(groups);
        }

        public string Id { get; set; }
        public string FacilityId { get; set; }
        [Required] 
        public DateTimeOffset StartTime { get; set; } = DateTimeOffset.Now;
        [Required] 
        public DateTimeOffset FinishTime { get; set; } = DateTimeOffset.Now;
        public IEnumerable<AuditItemGroupModel> Groups { get; } = Enumerable.Empty<AuditItemGroupModel>();
    }
}
