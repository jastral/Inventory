using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name e.g. Atharva Datar")]
        [Display(Name = "Name")]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage ="Enter a valid Email Address")]
        [EmailAddress]
        [Display(Name = "Enter your Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Password is necessary")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "Password too short. Minimum 6 length is required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(AllowEmptyStrings = false,ErrorMessage = "Please enter the confirm password")]
        [Compare("Password",ErrorMessage = "Confirm Password and Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}