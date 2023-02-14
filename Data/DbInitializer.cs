using Microsoft.EntityFrameworkCore;
using REST_API_TEMPLATE.Models;

namespace REST_API_TEMPLATE.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;

        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void Seed()
        { 
            _builder.Entity<Album>(a =>
            {
                a.HasData(new Album
                {
                    Id = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                    Name = "J.K. Rowling"                    
                });
                a.HasData(new Album
                {
                    Id = new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"),
                    Name = "Walter Isaacson"                    
                });
            });

            _builder.Entity<Image>(b =>
            {
                b.HasData(new Image
                {
                    Id = new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"),                    
                    Url = "Harry Potter and the Sorcerer's Stone",                    
                    Caption = "Scholastic; 1st Scholastic Td Ppbk Print., Sept.1999 edition (September 1, 1998)",                    
                    AlbumId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59")
                }); ;
                b.HasData(new Image
                {
                    Id = new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"),
                    Url = "Harry Potter and the Chamber of Secrets",                    
                    Caption = "Scholastic Paperbacks; Reprint edition (September 1, 2000)",                    
                    AlbumId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59")
                });
                b.HasData(new Image
                {
                    Id = new Guid("150c81c6-2458-466e-907a-2df11325e2b8"),
                    Url = "Steve Jobs",
                    Caption = "Simon & Schuster; 1st edition (October 24, 2011)",                    
                    AlbumId = new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd")
                });
            });
        }
    }
}
