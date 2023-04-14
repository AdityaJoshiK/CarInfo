namespace CarInfo.Areas.CAR_Image.Models
{
    public class CAR_ImageModel
    {
        public int? ImageID { get; set; }
        public int? CarID { get; set; }
        public List<IFormFile> Files { get; set; } // Update property name to "Files"
        public IFormFile File { get; set; } // Update property name to "Files"
        public string? PhotoPath { get; set; }
    }
}
