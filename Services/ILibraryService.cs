using REST_API_TEMPLATE.Models;

namespace REST_API_TEMPLATE.Services
{
    public interface ILibraryService
    {

        // Album Services
        Task<Album>         AddAlbumAsync(Album album);     // POST new album
        Task<List<Album>>   GetAlbumsAsync();               // GET all ablums
        Task<Album>   GetAlbumAsync(Guid id, bool includeImages = false); // GET album with(out) images
        Task<(bool, string)> DeleteAlbumAsync(Album album); // DELETE album

        // Image Services
        Task<Image>         AddImageAsync(Image image);     // POST new image
        Task<Image>         GetImageAsync(Guid id);         // GET specified image detail
        Task<(bool, string)> DeleteImageAsync(Image image); // DELETE image

    }
}
