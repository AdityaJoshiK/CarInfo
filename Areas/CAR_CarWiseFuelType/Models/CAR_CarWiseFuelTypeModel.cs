using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.CAR_CarWiseFuelType.Models
{
    public class CAR_CarWiseFuelTypeModel
    {
        public int? FuelTypeID { get; set; }
        public int? CarID { get; set; }
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Please enter FuelType name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("FuelType Name")]
        public string FuelTypeName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }

    public class CAR_CarWiseFuelTypeDropDownModel
    {
        public int FuelTypeID { get; set; }
        public string? FuelTypeName { get; set; }
    }
}
