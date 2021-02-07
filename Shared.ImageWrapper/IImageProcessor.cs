using System.Threading.Tasks;

namespace Shared.ImageWrapper
{
    public interface IImageProcessor
    {
        Task<byte[]> CompressAsync(byte[] imageBytes, int quality);
    }
}
