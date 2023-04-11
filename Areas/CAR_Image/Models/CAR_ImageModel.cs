namespace CarInfo.Areas.CAR_Image.Models
{
    public class CAR_ImageModel
    {
        public int? ImageID { get; set; }
        public int? CarID { get; set; }
        public IFormFile? File { get; set; }
        public string? PhotoPath { get; set; }
    }
}
