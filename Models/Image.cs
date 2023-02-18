using System.ComponentModel.DataAnnotations;

namespace REST_API_TEMPLATE.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Caption { get; set; }
        public string? Url { get; set; }
        // One-to-many relation with album
        public Guid AlbumId { get; set; }
        public Album? Album { get; set; }
    }

    // return values as dto
    public class ImageDto_LAI  // List all images within an album
    {
        public Guid id { get; set; }
        public string? url { get; set; }        
        public string? caption { get; set; }
    }

    public class ImageDto_UI    // Upload an image
    {
        public Guid id { get; set; }
        public string? url { get; set; }
        public Guid albumId { get; set; }
        public string? caption { get; set; }
    }

    public class ImageDto_GDI    // Get album detailed image
    {
        public Guid id { get; set; }
        public string? url { get; set; }
        public Guid albumId { get; set; }
        public string? caption { get; set; }
    }
}
