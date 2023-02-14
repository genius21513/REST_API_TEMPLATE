using Microsoft.EntityFrameworkCore;
using REST_API_TEMPLATE.Data;
using REST_API_TEMPLATE.Models;


namespace REST_API_TEMPLATE.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly AppDbContext _db;

        private readonly IWebHostEnvironment environment;

        public LibraryService(AppDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            this.environment = environment;
        }

        #region Albums


        public async Task<List<AlbumDto_LAA>> ListAlbumsAsync()
        {
            try
            {                
                return await _db.Albums
                    .Select(e => new AlbumDto_LAA { albumId = e.Id, albumName = e.Name })
                    .ToListAsync();
            } 
            catch (Exception ex) 
            {
                Console.WriteLine("Error: ", ex.Message);
                return null; 
            }
        }

        public async Task<AlbumDto_CA>  CreateAlbumAsync(Album album)
        {
            try
            {
                await _db.Albums.AddAsync(album);
                await _db.SaveChangesAsync();
                var dbAlbum = await _db.Albums.FindAsync(album.Id);

                if(dbAlbum == null) { return null; }
                return new AlbumDto_CA { id = dbAlbum.Id, name = dbAlbum.Name };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ", ex.Message);
                return null;
            }
        }

        public async Task<(bool, string)> DeleteAlbumAsync(Guid id)
        {
            try
            {
                var dbAlbum = await _db.Albums.FindAsync(id);
                if (dbAlbum == null) { return (false, "Album could not be found.");}
                _db.Albums.Remove(dbAlbum);
                await _db.SaveChangesAsync();
                return (true, "Album got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }


        public async Task<AlbumDto_LAI> ListAlbumImagesAsync(Guid id)
        {
            try
            {
                var images = await _db.Albums
                    .Select(a => new AlbumDto_LAI 
                    {
                        id = a.Id,
                        name = a.Name,
                        images = a.Images.Select(i => new ImageDto_LAI 
                        {
                            id = i.Id,
                            url = i.Url,
                            caption = i.Caption
                        }).ToList()
                    })
                    .Where(s => s.id == id)
                    .FirstAsync();

                //Console.WriteLine("images: ", images);
                return images;
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error: ", ex.Message);
                return null; 
            }
        }
        
        // non-routing function
        public async Task<Album> GetAlbumAsync(Guid id)
        {
            try
            {
                return await _db.Albums.FindAsync(id);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error: ", ex.Message);
                return null;
            }
        }


        #endregion Albums






        // Image Services

        #region Images



        public async Task<ImageDto_UI> UploadImageAsync(Image image)
        {
            try
            {
                await _db.Images.AddAsync(image);
                await _db.SaveChangesAsync();
                return await _db.Images
                        .Select(i => new ImageDto_UI
                        {
                            id = i.Id,
                            url = i.Url,
                            caption = i.Caption,
                            albumId = i.AlbumId
                        })
                        .Where(w => w.id == image.Id)
                        .FirstAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error: ", ex.Message);
                return null; 
            }
        }

        public async Task<(bool, string)> UploadImageAsync(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string path = "";
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    return (true, path);
                }
                else
                {
                    return (false, "Upload failed");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

        public async Task<(bool, string)> DeleteImageAsync(Guid id)
        {
            try
            {
                var dbAlbum = await _db.Images.FindAsync(id);

                if (dbAlbum == null)
                {
                    return (false, "Image could not be found.");
                }
                _db.Images.Remove(dbAlbum);
                await _db.SaveChangesAsync();
                return (true, "Image got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        public async Task<ImageDto_GDI> GetImageAsync(Guid aid, Guid iid)
        {
            try
            {
                return await _db.Images
                    .Select(i => new ImageDto_GDI
                    {
                        id= i.Id,
                        url = i.Url,
                        albumId = i.AlbumId,
                        caption = i.Caption
                    })
                    .Where(w => w.id == iid)
                    .FirstAsync();
                    //.FindAsync(iid);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error: ", ex.Message);
                return null; 
            }
        }

        // non-routing function
        public async Task<Image> GetImageAsync(Guid id)
        {
            try
            {
                return await _db.Images.FindAsync(id);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error: ", ex.Message);
                return null; 
            }
        }



        #endregion Images



    }
}