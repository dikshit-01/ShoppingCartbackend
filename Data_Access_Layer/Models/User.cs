using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class User:IdentityUser
    {
        [Required(ErrorMessage ="Plz Enter First Name")]
        public string FirstName { get; set; }=string.Empty;

        [Required(ErrorMessage = "Plz Enter Last Name")]
        public string LastName { get; set; } = string.Empty;
    }
}
