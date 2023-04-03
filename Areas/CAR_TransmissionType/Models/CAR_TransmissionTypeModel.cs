using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarInfo.Areas.CAR_TransmissionType.Models
{
    public class CAR_TransmissionTypeModel
    {
        public int? TransmissionTypeID { get; set; }

        [Required(ErrorMessage = "Please enter TransmissionType name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("TransmissionType Name")]
        public string TransmissionTypeName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
