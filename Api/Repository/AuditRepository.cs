using BlazorApp.Api.Entities;
using BlazorApp.Api.Interfaces;

namespace BlazorApp.Api.Repository
{
    public class AuditRepository : BaseRepository<Audit>, IAuditRepository
    {
        public AuditRepository(ICloudStorageSettings cloudStorageSettings, string tableName) : base(cloudStorageSettings, "Audits")
        {
        }
    }
}