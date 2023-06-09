﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.CAR_CarWiseFeature.Models
{
    public class CAR_CarWiseFeatureModel
    {
        public int? FeatureID { get; set; }
        public int? CarID { get; set; }
        public int? UserID { get; set; }
        public int? MakeID { get; set; }
        public int? VariantID { get; set; }

        [Required(ErrorMessage = "Please enter Feature name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Feature Name")]
        public string FeatureName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
