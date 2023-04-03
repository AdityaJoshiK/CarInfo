using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.CAR_Type.Models
{
    public class CAR_TypeModel
    {
        public int? TypeID { get; set; }

        [Required(ErrorMessage = "Please enter Type name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Type Name")]
        public string TypeName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
