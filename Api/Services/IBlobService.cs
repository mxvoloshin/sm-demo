using System;
using System.Threading.Tasks;
using BlazorApp.Shared;

namespace BlazorApp.Api.Services
{
    public interface IBlobService
    {
        Task<Uri> UploadImageAsync(ImageDto imageDto);
    }
}
