using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThinkBridgeUpdated.Models
{
    public class MemberViewMode
    {

        // Hello 0100 push
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "ProductName")]
        [Remote("IsProductNameAvailable", "Product")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Product Name can not be empty")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Product Name field must have minimum 5 and maximum 25 character!")]
        public string Brand { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Brand")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Brand  cannot be empty!")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Brand must have minimum 5 and maximum 15 character!")]

        public decimal Price { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Price")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Price  cannot be empty!")]



    }
}
