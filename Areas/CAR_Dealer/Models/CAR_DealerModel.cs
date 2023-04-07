using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.CAR_Dealer.Models
{
    public class CAR_DealerModel
    {
        public int? DealerID { get; set; }
        public int? MakeID { get; set; }
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Please enter Dealer name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Dealer Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
