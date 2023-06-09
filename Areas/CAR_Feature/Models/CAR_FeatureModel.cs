﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.CAR_Feature.Models
{
    public class CAR_FeatureModel
    {
        public int? FeatureID { get; set; }
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Please enter Feature name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Feature Name")]
        public string FeatureName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }

    public class CAR_FeatureDropDownModel
    {
        public int FeatureID { get; set; }
        public string? FeatureName { get; set; }
    }
}
