using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="Requerd")]
        [StringLength (50, ErrorMessage ="Must be less than 50 char")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.0, 999999.9, ErrorMessage = "Price must be a positive number with one decimal.")]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [DisplayName("Image")]
        public string ImgUrl { get; set; }

        
        [DisplayName("Category")]
        public int? CategoryId {  get; set; }
        public Category? Category { get; set; }
    }
}
