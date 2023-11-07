using AspNetCoreIdentity.Context;
using AspNetCoreIdentity.Entities;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
   
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly MyContext _myContext;
        private readonly RoleManager<AppRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, MyContext myContext, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _myContext = myContext;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            //var query = _userManager.Users;
            //var users = _myContext.Users.Join(_myContext.UserRoles, user => user.Id, userRole => userRole.UserId, (user, userRole) => new
            //{
            //    user,
            //    userRole
            //}).Join(_myContext.Roles, two => two.userRole.RoleId, role => role.Id, (two, role) => new { two.user, two.userRole, role }).Where(x => x.role.Name != "Admin").Select(x => new AppUser
            //{
            //    Id = x.user.Id,
            //    AccessFailedCount = x.user.AccessFailedCount,
            //    ConcurrencyStamp = x.user.ConcurrencyStamp,
            //    Email = x.user.Email,
            //    EmailConfirmed = x.user.EmailConfirmed,
            //    Gender = x.user.Gender,
            //    ImagePath = x.user.ImagePath,
            //    LockoutEnabled = x.user.LockoutEnabled,
            //    LockoutEnd = x.user.LockoutEnd,
            //    NormalizedEmail = x.user.NormalizedEmail,
            //    NormalizedUserName = x.user.NormalizedUserName,
            //    PasswordHash = x.user.PasswordHash,
            //    PhoneNumber = x.user.PhoneNumber,
            //    UserName = x.user.UserName

            //}).ToList();
            ////var users = await _userManager.GetUsersInRoleAsync("Member");

            //return View(users);
            List<AppUser> filteredUsers = new List<AppUser>();
            var users = _userManager.Users.ToList();
            foreach( var user in users)
            {
                var roles =await _userManager.GetRolesAsync(user);
                if(!roles.Contains("Admin"))
                {
                    filteredUsers.Add(user);
                }

            }
            return View(filteredUsers);
        }
        public IActionResult Create()
        {
            return View(new UserAdminCreateModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserAdminCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Gender = model.Gender,
                    Email = model.Email,
                    
                    
                };
               var result= await _userManager.CreateAsync(user, model.UserName + "123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.UserRole);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(" ", error.Description);
                    }
                }
               
            }
            

            return View(model);
        }
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == id);
            var userRoles =await _userManager.GetRolesAsync(user);
            var roles =  _roleManager.Roles.ToList();
            RolAssignSendModel model = new RolAssignSendModel();

            List<RolAssignlistModel> list = new List<RolAssignlistModel>();
            foreach (var role in roles)
            {
                list.Add(new()
                {
                    name = role.Name,
                    roleID = role.Id,
                    exist = userRoles.Contains(role.Name)
                });
            }
            model.Roles = list;
            model.userID = id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(RolAssignSendModel model)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == model.userID);
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach(var role in model.Roles)
            {
                if (role.exist)
                {
                    if (!userRoles.Contains(role.name))
                    {
                        await _userManager.AddToRoleAsync(user, role.name);
                    }
                }
                else
                {
                    if (userRoles.Contains(role.name))
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.name);
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}
