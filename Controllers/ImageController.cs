using Microsoft.AspNetCore.Mvc;
using REST_API_TEMPLATE.Models;
using REST_API_TEMPLATE.Services;

namespace REST_API_TEMPLATE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public ImageController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(Guid id)
        {
            Image image = await _libraryService.GetImageAsync(id);

            if (image == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No image found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, image);
        }

        [HttpPost]
        public async Task<ActionResult<Image>> AddImage(Image image)
        {
            var dbImage = await _libraryService.AddImageAsync(image);

            if (dbImage == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{image.Caption} could not be added.");
            }

            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            var image = await _libraryService.GetImageAsync(id);
            (bool status, string message) = await _libraryService.DeleteImageAsync(image);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, image);
        }
    }
}
