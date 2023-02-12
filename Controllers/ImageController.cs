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
            Book book = await _libraryService.GetBookAsync(id);

            if (book == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No book found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddImage(Image image)
        {
            var dbBook = await _libraryService.AddImageAsync(image);

            if (dbBook == null)
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
