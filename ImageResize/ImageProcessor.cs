using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace ImageResize
{
    public interface IImageProcessor
    {
        ImageResizeData ResizeImage(byte[] image);
    }

    public class ImageProcessor : IImageProcessor
    {
        public ImageResizeData ResizeImage(byte[] sourceImage)
        {
            IImageFormat format;
            var sharpImage = Image.Load(sourceImage, out format);
            int width = sharpImage.Width / 10;
            int height = sharpImage.Height / 10;

            using var stream = new MemoryStream();
            sharpImage.Mutate(x => x.Resize(width, height, KnownResamplers.Bicubic)); // Bicubic is the default algorithm
            //sharpImage.Mutate(x => x.Resize(width, height, KnownResamplers.NearestNeighbor)); // NearestNeighbor is the fastest algorithm, but offers low quality

            sharpImage.Save(stream, format);
            return new ImageResizeData
            {
                ImageData = stream.ToArray(),
                ContentType = format.DefaultMimeType,
                Extension = format.Name.ToLower()
            };
        }
    }
}
