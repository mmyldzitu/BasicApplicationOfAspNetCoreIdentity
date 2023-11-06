using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class UserSignInModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Parola Kısmı Boş Bırakılamaz")]
        public string Password { get; set; }

    }
}
