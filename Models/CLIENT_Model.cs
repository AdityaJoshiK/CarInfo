using System.Data;

namespace CarInfo.Models
{
    public class CLIENT_Model
    {
        public int? MakeID { get; set; }
        public int? CarID { get; set; }
        public int? MyCarID { get; set; }
        public String FuelTypeName { get; set; }
        public String TransmissionTypeName { get; set; }
        public int? TypeID { get; set; }
        public DataTable CarCategories { get; set; }
        public DataTable CarsByType { get; set; }
    }
}
