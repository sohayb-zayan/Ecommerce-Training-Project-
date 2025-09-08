using MyShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyShop.Entities.VMs
{
    public class ProductEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Must be less than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(0.0, 999999.9, ErrorMessage = "Price must be a positive number with one decimal.")]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        [BindNever]
        public IFormFile? ImgFile { get; set; }  

        [DisplayName("Image URL")]
        [ValidateNever]
        public string ImgUrl { get; set; } 

        [Required(ErrorMessage = "Required")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

 
    }
}
