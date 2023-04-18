using System.Data;

namespace CarInfo.Models
{
    public class Car_Feature
    {
        public int FeatureID { get; set; }
        public int CarID { get; set; }
        public string FeatureName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public DataTable CarCategories { get; set; }
        public DataTable CarsByType { get; set; }
    }

}
