using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_API_TEMPLATE.Models;
using REST_API_TEMPLATE.Requests;
using REST_API_TEMPLATE.Services;

namespace REST_API_TEMPLATE.Controllers
{
    //[Route("/[controller]")]    
    [Route("/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        
        public AlbumController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        /// <summary>
        /// Gets the list of all Albums.
        /// </summary>        
        // GET: /albums
        [HttpGet]
        public async Task<IActionResult> ListAblums()
        {
            var albums = await _libraryService.ListAlbumsAsync();

            if (albums == null)
            {                
                return StatusCode(StatusCodes.Status204NoContent, "No albums in database");
            }

            return StatusCode(StatusCodes.Status200OK, albums);
        }

        /// <summary>
        /// Get list of all images within an album
        /// </summary>
        [HttpGet("{album_id}")]
        public async Task<IActionResult> ListAlbumImages([FromRoute] Guid album_id)
        {
            var album = await _libraryService.ListAlbumImagesAsync(album_id);

            if (album == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No image found");
            }

            return StatusCode(StatusCodes.Status200OK, album);
        }

        /// <summary>
        /// Create an album
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromBody] CreateAlbumRequest car)
        {
            Album album = new Album { 
                Id = new Guid(), 
                Name = car.Name 
            };

            var dbAlbum = await _libraryService.CreateAlbumAsync(album);

            if (dbAlbum == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{album.Name} could not be added.");
            }

            return StatusCode(StatusCodes.Status200OK, dbAlbum);            
        }

        /// <summary>
        /// Delete an album
        /// </summary>
        [HttpDelete("{album_id}")]
        public async Task<IActionResult> DeleteAlbum([FromRoute]Guid album_id)
        {
            var album = await _libraryService.GetAlbumAsync(album_id);

            (bool status, string message) = await _libraryService.DeleteAlbumAsync(album_id);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, $"Deleted album id: {album_id}");
        }
    }
}
