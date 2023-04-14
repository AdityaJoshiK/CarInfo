using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.MST_Car.Models
{
    public class MST_CarModel
    {
        public int? CarID { get; set; }
        public int? MakeID { get; set; }
        public int? TypeID { get; set; }
        public int? VariantID { get; set; }
        public List<int> FuelTypeID { get; set; }

        public int? TransmiTypeID { get; set; }
        public int? UserID { get; set; }
        public int? ImageID { get; set; }

        [Required(ErrorMessage = "Please enter Car name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Car Name")]
        public string Name { get; set; }
        public string FeatureName { get; set; }
        public List<string> FuelTypeName { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }

    public class MST_CarDropDownModel
    {
        public int CarID { get; set; }
        public string? CarName { get; set; }
    }
}
