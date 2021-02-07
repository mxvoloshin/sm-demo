using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Shared.ImageWrapper
{
    public class ImageProcessor : IImageProcessor
    {
        public async Task<byte[]> CompressAsync(byte[] imageBytes, int quality)
        {
            using (var image = Image.Load(Configuration.Default, imageBytes))
            {
                using (var ms = new MemoryStream())
                {
                    await image.SaveAsJpegAsync(ms, new JpegEncoder { Quality = quality });
                    return ms.ToArray();
                }
            }
        }
    }
}
