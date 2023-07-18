using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Confirm Password is not matching with Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassword { get; set;}
    }
}
