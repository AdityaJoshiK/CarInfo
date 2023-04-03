using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.CAR_FuelType.Models
{
    public class CAR_FuelTypeModel
    {
        public int? FuelTypeID { get; set; }

        [Required(ErrorMessage = "Please enter FuelType name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("FuelType Name")]
        public string FuelTypeName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
