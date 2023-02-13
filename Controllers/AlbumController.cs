using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_API_TEMPLATE.Models;
using REST_API_TEMPLATE.Services;

namespace REST_API_TEMPLATE.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public AlbumController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAblums()
        {
            var albums = await _libraryService.GetAlbumsAsync();

            if (albums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No albums in database");
            }

            return StatusCode(StatusCodes.Status200OK, albums);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAlbum(Guid id, bool includeAlbums = false)
        {
            Album album = await _libraryService.GetAlbumAsync(id, includeAlbums);

            if (album == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Album found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, album);
        }

        [HttpPost]
        public async Task<ActionResult<Album>> AddAlbum(Album album)
        {
            var dbAlbum = await _libraryService.AddAlbumAsync(album);

            if (dbAlbum == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{album.Name} could not be added.");
            }

            return CreatedAtAction("GetAlbum", new { id = album.Id }, album);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var album = await _libraryService.GetAlbumAsync(id, false);
            (bool status, string message) = await _libraryService.DeleteAlbumAsync(album);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, album);
        }
    }
}
