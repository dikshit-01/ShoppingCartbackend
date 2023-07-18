using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class UserLoginDto
    {
        [Required(ErrorMessage ="Plz Enter Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="plz Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
