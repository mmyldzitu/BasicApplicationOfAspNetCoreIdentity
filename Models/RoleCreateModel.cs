using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class RoleCreateModel
    {
        [Required(ErrorMessage ="Ad Alanı Gereklidir")]
        public string Name { get; set; }
    }
}
