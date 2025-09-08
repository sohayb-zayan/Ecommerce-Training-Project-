using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.VMs
{
      public class RigVM
    {
        [Required(ErrorMessage ="User name is requerd& unique")]
        public string UserName { get; set; }


        [Required (ErrorMessage ="Must set password")]
        [StringLength(40,MinimumLength =8,ErrorMessage ="must be more thab 8 and less than 40 char") ]
        [DataType(DataType.Password)]
        [Compare ("ConfirmPassword", ErrorMessage = "pass word desont match.")]
        public string Password { get; set; }

        [Required (ErrorMessage ="must set")]
        [DataType(DataType.Password)]
        [Display (Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "must set")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "must be more than 1 and less than 10 char") ]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "must set")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "must be more than 1 and less than 10 char") ]
        public string LasttName { get; set; }


        [Required (ErrorMessage ="Must be set")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Must be set")]
        [StringLength(20, ErrorMessage = "Country name cannot exceed 20 characters")]
        public string Country {  get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be 11 digits and start with 010, 011, 012, or 015")]
        public string PhoneNumber { get; set; }

        


    }
}
