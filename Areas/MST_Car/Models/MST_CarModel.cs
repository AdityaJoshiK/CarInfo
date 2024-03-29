﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;

namespace CarInfo.Areas.MST_Car.Models
{
    public class MST_CarModel
    {
        public int? CarID { get; set; }
        public int? MakeID { get; set; }
        public int? TypeID { get; set; }
        public int? VariantID { get; set; }
        public List<int> FuelTypeID { get; set; }

        public int? TransmiTypeID { get; set; }
        public int? UserID { get; set; }
        public int? ImageID { get; set; }

        [Required(ErrorMessage = "Please enter Car name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Car Name")]
        public string Name { get; set; }
        public string FeatureName { get; set; }
        public List<string> FuelTypeName { get; set; }
        public IFormFile? File { get; set; }
        public string? PhotoPath { get; set; }
        public int Year { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public DataTable FeatureDataTable { get; set; }
        public DataTable CarDetailDataTable { get; set; }
        public DataTable CarVariantsDataTable { get; set; }

        public List<string> features { get; set; }
    }

    public class MST_CarDropDownModel
    {
        public int CarID { get; set; }
        public string? Name { get; set; }
    }
}
