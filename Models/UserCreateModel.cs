using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage="Kullanıcı Adı Boş Bırakılamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Kısmı Boş Bırakılamaz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola Alanı Boş Bırakılamaz")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Parola Eşleşme hatası")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Cinsiyet Kısmı Boş Bırakılamaz")]
        public string Gender { get; set; }
    }
}
