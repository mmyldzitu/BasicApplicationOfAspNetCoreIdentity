using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class RolAssignlistModel
    {
        public int roleID { get; set; }
        public string name { get; set; }
        public bool exist { get; set; }
    }
    public class RolAssignSendModel
    {
        public List<RolAssignlistModel> Roles { get; set; }
        public int userID { get; set; }
    }
}
