using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.CAR_CarWiseTransmissionType.Models
{
    public class CAR_CarWiseTransmissionTypeModel
    {
        public int? TransmissionTypeID { get; set; }
        public int? CarID { get; set; }

        [Required(ErrorMessage = "Please enter TransmissionType name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("TransmissionType Name")]
        public string TransmissionTypeName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
