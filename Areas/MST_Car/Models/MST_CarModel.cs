using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.MST_Car.Models
{
    public class MST_CarModel
    {
        public int? CarID { get; set; }
        public int? MakeID { get; set; }
        public int? TypeID { get; set; }
        public int? FuelTypeID { get; set; }
        public int? TransmiTypeID { get; set; }
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Please enter Car name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Car Name")]
        public string Name { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
