using Microsoft.EntityFrameworkCore;
using REST_API_TEMPLATE.Data;
using REST_API_TEMPLATE.Models;

namespace REST_API_TEMPLATE.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly AppDbContext _db;

        public LibraryService(AppDbContext db)
        {
            _db = db;
        }

        #region Albums
        public async Task<List<Album>> GetAlbumsAsync()
        {
            try
            {
                return await _db.Albums.ToListAsync();
            } catch (Exception ex) { return null; }
        }

        public async Task<Album> AddAlbumAsync(Album album)
        {
            try
            {
                await _db.Albums.AddAsync(album);
                await _db.SaveChangesAsync();
                return await _db.Albums.FindAsync(album.Id);
            }
            catch (Exception ex){ return null; }
        }

        public async Task<(bool, string)> DeleteAlbumAsync(Album album)
        {
            try
            {
                var dbAlbum = await _db.Albums.FindAsync(album.Id);

                if (dbAlbum == null)
                {
                    return (false, "Album could not be found.");
                }
                _db.Albums.Remove(album);
                await _db.SaveChangesAsync();
                return (true, "Album got deleted.");
            } catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
        
        public async Task<Album> GetAlbumAsync(Guid id, bool includeImages)
        {
            try
            {
                if (includeImages)
                {
                    return await _db.Albums.Include(b => b.Images)
                        .FirstOrDefaultAsync(i => i.Id == id);
                }

                return await _db.Albums.FindAsync(id);
            }catch(Exception ex) { return null;  }
        }

        #endregion Albums



        // Image Services

        #region Images

        public async Task<Image> AddImageAsync(Image image)
        {
            try
            {
                await _db.Images.AddAsync(image);
                await _db.SaveChangesAsync();
                return await _db.Images.FindAsync(image.Id); // Auto ID from DB
            }
            catch (Exception ex) { return null; }
        }
        
        public async Task<Image> GetImageAsync(Guid id)
        {
            try
            {
                return await _db.Images.FindAsync(id);       
            }
            catch (Exception ex) { return null; }
        }   

        public async Task<(bool, string)> DeleteImageAsync(Image image)
        {
            try
            {
                var dbAlbum = await _db.Images.FindAsync(image.Id);

                if (dbAlbum == null)
                {
                    return (false, "Image could not be found.");
                }
                _db.Images.Remove(image);
                await _db.SaveChangesAsync();
                return (true, "Image got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Images
    }
}