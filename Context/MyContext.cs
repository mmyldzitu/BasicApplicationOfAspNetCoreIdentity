using AspNetCoreIdentity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Context
{
    public class MyContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }
    }
}
