using REST_API_TEMPLATE.Models;

namespace REST_API_TEMPLATE.Services
{
    public interface ILibraryService
    {

        // Album Services
        Task<List<AlbumDto_LAA>?>      ListAlbumsAsync();     // Create an album
        Task<AlbumDto_CA>       CreateAlbumAsync(Album album);     // Create an album
        Task<AlbumDto_LAI>      ListAlbumImagesAsync(Guid id);         // GET album images with id
        Task<(bool, string)>    DeleteAlbumAsync(Guid id);      // DELETE album

        // non-routing function
        Task<Album> GetAlbumAsync(Guid id); // Get image by id




        // Image Services
        Task<bool> AddImageAsync(Image image);     // Upload an image
        //Task<(bool, string)> UploadImageAsync(IFormFile file);
        Task<(bool, string)> DeleteImageAsync(Guid id); // DELETE image
        Task<ImageDto_GDI> GetImageAsync(Guid aid, Guid iid);         // GET specified image by album, image id

        // non-routing function
        Task<Image> GetImageAsync(Guid iid);         // GET imag by id




        // aws s3 bucket
        //Task<byte[]> DownloadFileAsync(string file);
        Task<bool> UploadFileAsync(IFormFile file);
        Task<bool> DeleteFileAsync(string fileName);
        Task<string> GetFileUrlAsync(string fileName);
    }
}
