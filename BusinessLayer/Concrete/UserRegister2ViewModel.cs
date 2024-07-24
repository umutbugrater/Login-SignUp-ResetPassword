using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserRegister2ViewModel
    {
        public string? ImageUrl { get; set; }

        public string? NameSurname { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
