using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;

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


        [Required(ErrorMessage = "Please enter Review"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Review Text")]
        public string ReviewText { get; set; }

        public int? Rating { get; set; }
        public DataTable CarCategories { get; set; }
        public DataTable CarsByType { get; set; }
    }
}
