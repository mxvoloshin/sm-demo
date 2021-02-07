using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Client.Extensions
{
    public static class BrowserFileExtensions
    {
        const long MaxImageSize = 4 * 1024 * 1024;

        public static async Task<string> GetPreviewUrlAsync(this IBrowserFile file, int size, CancellationToken cancellationToken = default)
        {
            var buffer = await file.GetResizedImageAsync(size, cancellationToken);
            return $"data:image/jpeg;base64,{Convert.ToBase64String(buffer)}";
        }

        public static async Task<byte[]> GetResizedImageAsync(this IBrowserFile file, int size, CancellationToken cancellationToken = default)
        {
            var resizedImage = await file.RequestImageFileAsync("image/jpeg", size, size);
            var buffer = new byte[resizedImage.Size];
            using (var stream = resizedImage.OpenReadStream(MaxImageSize, cancellationToken))
            {
                await stream.ReadAsync(buffer, cancellationToken);
            }

            return buffer;
        }
    }
}
