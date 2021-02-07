using BlazorApp.Api.Entities;
using BlazorApp.Api.Interfaces;

namespace BlazorApp.Api.Repository
{
    public class FacilityRepository : BaseRepository<Facility>, IFacilityRepository
    {
        public FacilityRepository(ICloudStorageSettings cloudStorageSettings) : base(cloudStorageSettings, "Facilities")
        {
        }
    }
}
