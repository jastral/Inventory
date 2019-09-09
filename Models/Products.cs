using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name cannot be empty!")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "0")]
        public int Quantity { get; set; }

        
        public string ProductDescription { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload file")]
        [Required(ErrorMessage = "Please choose a file to upload.")]
        public HttpPostedFileBase Image { get; set; }
    }
}