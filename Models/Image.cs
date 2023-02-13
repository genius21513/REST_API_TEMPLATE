using System.ComponentModel.DataAnnotations;

namespace REST_API_TEMPLATE.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string? Url { get; set; }        
        public string? Description { get; set; }
        public string? Caption { get; set; }

        // One-to-many relation with album
        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; }
    }
}
