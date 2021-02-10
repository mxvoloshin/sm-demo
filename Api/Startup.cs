using BlazorApp.Api;
using BlazorApp.Api.Interfaces;
using BlazorApp.Api.Repository;
using BlazorApp.Api.Services;
using BlazorApp.Api.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shared.ImageWrapper;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BlazorApp.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<ICloudStorageSettings, AppSettings>();
            builder.Services.AddScoped<IDateTimeService, DateTimeService>();

            builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
            builder.Services.AddScoped<IBlobService, BlobService>();
            builder.Services.AddScoped<IImageProcessor, ImageProcessor>();
        }
    }
}
