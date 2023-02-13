using Microsoft.AspNetCore.Mvc;
using REST_API_TEMPLATE.Models;
using REST_API_TEMPLATE.Services;

namespace REST_API_TEMPLATE.Controllers
{
    [Route("/albums")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public ImageController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpPost("{album_id}/images")]
        public async Task<ActionResult<Image>> UploadImage(Image image)
        {
            var dbImage = await _libraryService.UploadImageAsync(image);

            if (dbImage == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{image.Caption} could not be added.");
            }

            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        [HttpGet("{album_id}/images/{image_id}")]
        public async Task<IActionResult> GetImage(Guid album_id, Guid image_id)
        {
            var image = await _libraryService.GetImageAsync(album_id, image_id);

            if (image == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No book found for id: {image.id}");
            }

            return StatusCode(StatusCodes.Status200OK, image);
        }

        [HttpDelete("{album_id}/images/{image_id}")]
        public async Task<IActionResult> DeleteImage(Guid album_id, Guid image_id)
        {
            (bool status, string message) = await _libraryService.DeleteImageAsync(image_id);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, $"Deleted image {image_id}");
        }
    }
}
