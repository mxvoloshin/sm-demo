using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared
{
    public class FacilityDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Address { get; set; }
    }
}
