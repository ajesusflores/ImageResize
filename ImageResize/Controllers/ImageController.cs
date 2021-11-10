using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            var bytes = _imageProcessor.ResizeImage(memoryStream.ToArray());
            return File(bytes.ImageData, "image/jpeg", "test.jpg");
        }
    }
}
