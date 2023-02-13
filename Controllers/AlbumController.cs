using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_API_TEMPLATE.Models;
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
        /// <returns>The list of Albums.</returns>
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


        [HttpGet("{album_id}")]
        public async Task<IActionResult> ListAlbumImages(Guid album_id)
        {
            var album = await _libraryService.ListAlbumImagesAsync(album_id);

            if (album == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No image found");
            }

            return StatusCode(StatusCodes.Status200OK, album);
        }

        /// <summary>
        /// Creates an Employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Employee
        ///     {        
        ///       "name": "Mike",        
        ///     }
        /// </remarks>
        /// <param name="album"></param> 
        [HttpPost]
        public async Task<IActionResult> CreateAlbum(Album album)
        {
            var dbAlbum = await _libraryService.CreateAlbumAsync(album);

            if (dbAlbum == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{album.Name} could not be added.");
            }

            return StatusCode(StatusCodes.Status200OK, dbAlbum);

            //return CreatedAtAction("GetAlbum", new { id = dbAlbum.id }, dbAlbum);
        }

        [HttpDelete("{album_id}")]
        public async Task<IActionResult> DeleteAlbum(Guid album_id)
        {
            var album = await _libraryService.GetAlbumAsync(album_id);
            (bool status, string message) = await _libraryService.DeleteAlbumAsync(album_id);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, $"Delted album id: {album_id}");
        }
    }
}
