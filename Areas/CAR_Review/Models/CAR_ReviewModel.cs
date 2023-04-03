using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.CAR_Review.Models
{
    public class CAR_ReviewModel
    {
        public int? ReviewID { get; set; }
        public int? CarID { get; set; }

        [Required(ErrorMessage = "Please enter Review"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Review Text")]
        public string ReviewText { get; set; }

        public int Rating { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
