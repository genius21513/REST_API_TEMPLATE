using Microsoft.AspNetCore.Mvc;
using REST_API_TEMPLATE.Models;
using REST_API_TEMPLATE.Requests;
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



        /// <summary>
        /// Upload a image
        /// </summary>
        [HttpPost("{album_id}/images")]
        public async Task<ActionResult<ImageDto_UI>> UploadImage([FromForm] UploadImageRequest uir)
        {
            try
            {
                var key = uir.file.FileName;
                await _libraryService.UploadFileAsync(uir.file);
                var pUrl = await _libraryService.GetFileUrlAsync(key);

                var image = new Image
                {
                    AlbumId = uir.album_id,
                    Caption = uir.caption,                    
                    Url = pUrl,
                    Name = key  // set name as file name
                };
                await _libraryService.AddImageAsync(image);                

                return StatusCode(StatusCodes.Status200OK,
                    new ImageDto_UI
                    {
                        id = image.Id,
                        url = pUrl,
                        albumId = image.AlbumId,
                        caption = image.Caption
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }

        /// <summary>
        /// Get a specified image from an album
        /// </summary>
        [HttpGet("{album_id}/images/{image_id}")]
        public async Task<IActionResult> GetImage(Guid album_id, Guid image_id)
        {
            var image = await _libraryService.GetImageAsync(album_id, image_id);

            if (image == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No book found for id: {image_id}");
            }

            return StatusCode(StatusCodes.Status200OK, image);
        }

        /// <summary>
        /// Delete a image from an album
        /// </summary>
        [HttpDelete("{album_id}/images/{image_id}")]
        public async Task<IActionResult> DeleteImage(Guid album_id, Guid image_id)
        {
            var image = await _libraryService.GetImageAsync(image_id);

            await _libraryService.DeleteFileAsync(image.Url);

            (bool status, string message) = await _libraryService.DeleteImageAsync(image_id);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, $"Deleted image {image_id}");
        }
    }
}
