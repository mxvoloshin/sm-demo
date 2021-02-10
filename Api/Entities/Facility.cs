namespace BlazorApp.Api.Entities
{
    public class Facility : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PreviewUrl { get; set; }
    }
}
