using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarInfo.Areas.CAR_Make.Models
{
    public class CAR_MakeModel
    {
        public int? MakeID { get; set; }

        [Required(ErrorMessage = "Please enter Make name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Make Name")]
        public string MakeName { get; set; }

        public IFormFile? File { get; set; }
        public string? PhotoPath { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }

    public class CAR_MakeDropDownModel
    {
        public int MakeID { get; set; }
        public string? MakeName { get; set; }
    }
}
