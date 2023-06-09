﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarInfo.Areas.CAR_Variant.Models
{
    public class CAR_VariantModel
    {
        public int? VariantID { get; set; }
        public int? MakeID { get; set; }
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Please enter Variant name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Variant Name")]
        public string VariantName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }  
    }

    public class CAR_VariantDropDownModel
    {
        public int VariantID { get; set; }
        public string? VariantName { get; set; }

        public bool isSelected { get; set; } // Add isSelected property
    }
}
