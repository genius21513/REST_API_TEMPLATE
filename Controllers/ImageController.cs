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
        public async Task<ActionResult<ImageDto_UI>> UploadImage([FromRoute] Guid albumId, [FromForm] UploadImageRequest uir)
        {
            try
            {
                (bool status, string path) = await _libraryService.UploadImageAsync(uir.file);

                var image = new Image { 
                    AlbumId = albumId,
                    Caption = uir.caption,
                    Url = path
                };                

                if (status)
                {
                    await _libraryService.UploadImageAsync(image);
                    return StatusCode(StatusCodes.Status200OK, 
                        new ImageDto_UI {
                            id = image.Id,
                            url = image.Url,
                            albumId = image.AlbumId,
                            caption = image.Caption
                        });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "File Upload Failed");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
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
