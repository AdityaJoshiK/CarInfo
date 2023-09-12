using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarInfo.Models
{
    public class CLIENT_Model
    {
        public int? MakeID { get; set; }
        public int? CarID { get; set; }
        public int? MyCarID { get; set; }
        public String FuelTypeName { get; set; }
        public String TransmissionTypeName { get; set; }
        public int? TypeID { get; set; }

        public string? minPrice { get; set; }
        public string? maxPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter Review"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Review Text")]
        public string ReviewText { get; set; }

        public string PriceRange { get; set; }

        public int? Rating { get; set; }
        public DataTable CarCategories { get; set; }
        public DataTable CarsByType { get; set; }
    }
}
