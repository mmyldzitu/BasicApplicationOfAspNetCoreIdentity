using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class UserAdminCreateModel
    {
        [Required(ErrorMessage ="Kullanıcı Adi Boş Bırakılamaz")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Email kısmı boş bırakılamaz")]
        public string Email { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage ="Rol kısmı boş bırakılamaz")]
        public string UserRole { get; set; }
    }
}
