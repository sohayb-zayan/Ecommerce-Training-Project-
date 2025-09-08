using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Model
{
    public class Useres:IdentityUser
    {
        [Required (ErrorMessage ="Must be")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Must be")]
        public string? LastName { get; set; }
        [Required(ErrorMessage ="must be")]
        public string? Country { get; set; }

    }
}
