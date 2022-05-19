using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UkraineRestorationArchive.WEB.Models
{
    public class LoginModel
    {
        [Display(Name = "Email addres")]
        [DataType(DataType.EmailAddress)]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
