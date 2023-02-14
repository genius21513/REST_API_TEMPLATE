using Microsoft.AspNetCore.Mvc;

namespace REST_API_TEMPLATE.Requests
{
    public class UploadImageRequest
    {
        [FromRoute]
        public Guid album_id { get; set; }

        //[FromBody]


        [FromForm]
        public string caption { get; set; }
        public IFormFile file { get; set; }
    }
}
