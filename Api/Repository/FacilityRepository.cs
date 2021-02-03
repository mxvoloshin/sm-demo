using BlazorApp.Api.Entities;
using BlazorApp.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Api.Repository
{
    public class FacilityRepository : BaseRepository<Facility>, IFacilityRepository
    {
        public FacilityRepository(ICloudStorageSettings cloudStorageSettings) : base(cloudStorageSettings, "Facilities")
        {
        }
    }
}
