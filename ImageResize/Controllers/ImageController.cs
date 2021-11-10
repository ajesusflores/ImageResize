using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;

namespace ImageResize.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IImageProcessor _imageProcessor;

        public ImageController(ILogger<ImageController> logger, IImageProcessor imageProcessor)
        {
            _logger = logger;
            _imageProcessor = imageProcessor;
        }

        [HttpPost]
        [Route("resize")]
        public IActionResult Resize(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            var data = _imageProcessor.ResizeImage(memoryStream.ToArray());
            return File(data.ImageData, data.ContentType, $"test.{data.Extension}");
        }
    }
}
