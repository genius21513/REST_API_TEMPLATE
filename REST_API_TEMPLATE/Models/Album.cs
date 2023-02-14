using System.ComponentModel.DataAnnotations;

namespace REST_API_TEMPLATE.Models
{

    public class Album
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }

        // One-to-many relationship with imagesd
        public List<Image>? Images { get; set; }
    }


    // return values as dtos
    public class AlbumDto_LAA   // List All Albums
    {
        public Guid albumId { get; set; }
        public string? albumName { get; set; }
    }

    public class AlbumDto_LAI  // List all images within an album
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public List<ImageDto_LAI>? images { get; set; }
    }

    public class AlbumDto_CA    // Create album
    {
        public Guid id { get; set; }
        public string? name { get; set; }
    }
}
