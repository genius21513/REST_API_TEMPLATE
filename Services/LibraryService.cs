using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API_TEMPLATE.Data;
using REST_API_TEMPLATE.Models;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Amazon.S3.Model;

namespace REST_API_TEMPLATE.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly AppDbContext _db;

        private readonly IWebHostEnvironment environment;
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public LibraryService(AppDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            this.environment = environment;

            AmazonS3Client s3Client = new AmazonS3Client(
                "AKIA3OPVBJAQ7UN7NKXN",
                "Rqyciz+0VO+LLm7hRW39E1dvgvaMzqsarnfxg25B",
                Amazon.RegionEndpoint.USEast1
            );
            _s3Client = s3Client;
            _bucketName = "amuham47-dotnet-bucket";
        }




        #region Albums


        public async Task<List<AlbumDto_LAA>?> ListAlbumsAsync()
        {
            try
            {
                return await _db.Albums
                    .Select(e => new AlbumDto_LAA
                    {
                        albumId = e.Id,
                        albumName = e.Name
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ", ex.Message);
                return null;
            }
        }

        public async Task<AlbumDto_CA> CreateAlbumAsync(Album album)
        {
            try
            {
                await _db.Albums.AddAsync(album);
                await _db.SaveChangesAsync();
                var dbAlbum = await _db.Albums.FindAsync(album.Id);

                if (dbAlbum == null)
                {
                    return null;
                }

                return new AlbumDto_CA { 
                    id = dbAlbum.Id, 
                    name = dbAlbum.Name 
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<(bool, string)> DeleteAlbumAsync(Guid id)
        {
            try
            {
                var dbAlbum = await _db.Albums.FindAsync(id);

                if (dbAlbum == null) { 
                    return (false, "Album could not be found."); 
                }

                var images = _db.Images.Where(i => i.AlbumId == id);

                foreach (var image in images)
                {
                    dbAlbum.Images.Remove(image);

                    await _s3Client.DeleteObjectAsync(_bucketName, image.Name);
                }

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
                
                return images;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        #endregion Albums








        // Image Services

        #region Images


        public async Task<bool> AddImageAsync(Image image)
        {
            try
            {
                await _db.Images.AddAsync(image);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
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
                        id = i.Id,
                        url = i.Url,
                        albumId = i.AlbumId,
                        caption = i.Caption
                    })
                    .Where(w => w.id == iid)
                    .FirstAsync();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
                return null;
            }
        }



        #endregion Images



        #region aws
        public async Task<bool> UploadFileAsync(IFormFile file)
        {
            try
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = _bucketName,
                        ContentType = file.ContentType
                    };

                    var fileTransferUtility = new TransferUtility(_s3Client);

                    await fileTransferUtility.UploadAsync(uploadRequest);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public async Task<bool> DeleteFileAsync(string fileName)
        {
            try
            {
                var key = fileName;
                await _s3Client.DeleteObjectAsync(_bucketName, key);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<string> GetFileUrlAsync(string fileName)
        {
            try
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = _bucketName,
                    Key = fileName,
                    Expires = DateTime.UtcNow.AddMinutes(8640)
                };

                return _s3Client.GetPreSignedURL(urlRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        #endregion aws

    }
}