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

        //#region Authors

        //public async Task<List<Author>> GetAuthorsAsync()
        //{
        //    try
        //    {
        //        return await _db.Authors.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public async Task<Author> GetAuthorAsync(Guid id, bool includeBooks)
        //{
        //    try
        //    {
        //        if (includeBooks) // books should be included
        //        {
        //            return await _db.Authors.Include(b => b.Books)
        //                .FirstOrDefaultAsync(i => i.Id == id);
        //        }

        //        // Books should be excluded
        //        return await _db.Authors.FindAsync(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public async Task<Author> AddAuthorAsync(Author author)
        //{
        //    try
        //    {
        //        await _db.Authors.AddAsync(author);
        //        await _db.SaveChangesAsync();
        //        return await _db.Authors.FindAsync(author.Id); // Auto ID from DB
        //    }
        //    catch (Exception ex)
        //    {
        //        return null; // An error occured
        //    }
        //}

        //public async Task<Author> UpdateAuthorAsync(Author author)
        //{
        //    try
        //    {
        //        _db.Entry(author).State = EntityState.Modified;
        //        await _db.SaveChangesAsync();

        //        return author;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public async Task<(bool, string)> DeleteAuthorAsync(Author author)
        //{
        //    try
        //    {
        //        var dbAuthor = await _db.Authors.FindAsync(author.Id);

        //        if (dbAuthor == null)
        //        {
        //            return (false, "Author could not be found");
        //        }

        //        _db.Authors.Remove(author);
        //        await _db.SaveChangesAsync();

        //        return (true, "Author got deleted.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, $"An error occured. Error Message: {ex.Message}");
        //    }
        //}

        //#endregion Authors

        //#region Books

        //public async Task<List<Book>> GetBooksAsync()
        //{
        //    try
        //    {
        //        return await _db.Books.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public async Task<Book> GetBookAsync(Guid id)
        //{
        //    try
        //    {
        //        return await _db.Books.FindAsync(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public async Task<Book> AddBookAsync(Book book)
        //{
        //    try
        //    {
        //        await _db.Books.AddAsync(book);
        //        await _db.SaveChangesAsync();
        //        return await _db.Books.FindAsync(book.Id); // Auto ID from DB
        //    }
        //    catch (Exception ex)
        //    {
        //        return null; // An error occured
        //    }
        //}

        //public async Task<Book> UpdateBookAsync(Book book)
        //{
        //    try
        //    {
        //        _db.Entry(book).State = EntityState.Modified;
        //        await _db.SaveChangesAsync();

        //        return book;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public async Task<(bool, string)> DeleteBookAsync(Book book)
        //{
        //    try
        //    {
        //        var dbBook = await _db.Books.FindAsync(book.Id);

        //        if (dbBook == null)
        //        {
        //            return (false, "Book could not be found.");
        //        }

        //        _db.Books.Remove(book);
        //        await _db.SaveChangesAsync();

        //        return (true, "Book got deleted.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, $"An error occured. Error Message: {ex.Message}");
        //    }
        //}

        //#endregion Books



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