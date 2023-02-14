namespace REST_API_TEMPLATE.Requests
{
    public class UploadImageRequest
    {        
        public string caption { get; set; }        
        public IFormFile file { get; set; }
    }
}
