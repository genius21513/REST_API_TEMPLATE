using System.ComponentModel.DataAnnotations;

namespace REST_API_TEMPLATE.Models
{

    public class Album
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }

        // One-to-many relationship with books
        public List<Image>? Images { get; set; }
    }
}
