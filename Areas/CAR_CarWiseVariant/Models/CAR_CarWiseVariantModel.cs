using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.CAR_CarWiseVariant.Models
{
    public class CAR_CarWiseVariantModel
    {
        public int? CarWiseVariantID { get; set; }
        public int? CarID { get; set; }
        public int? MakeID { get; set; }
        public int? UserID { get; set; }

        public int? VariantID { get; set; }

        public decimal Price { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
