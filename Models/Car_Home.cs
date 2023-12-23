using System.Data;

namespace CarInfo.Models
{
    public class Car_Home : CLIENT_Model
    {
        //public int? MakeID { get; set; }
        public List<CLIENT_Model> CarList { get; set; }
        public DataTable MakeDataTable { get; set; }
        public DataTable MakerPhotoDataTable { get; set; }
        public DataTable ReviewDataTable { get; set; }
    }

}
