﻿using AspNetCoreIdentity.Entities;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Create()
        {
            return View( new UserCreateModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Email = model.Email,
                    Gender=model.Gender,
                    UserName=model.UserName
                };
                
                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded)
                {
                    var memberRole = await _roleManager.FindByNameAsync("Member");
                    if (memberRole==null)
                    {
                        await _roleManager.CreateAsync(new()
                        {
                            Name = "Member",
                            CreatedTime = DateTime.Now
                        });
                    }
                    
                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Index");

                }
                foreach ( var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        public IActionResult SignIn(string returnURL)
        {
            return View(new UserSignInModel() { ReturnURl = returnURL });
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            if (ModelState.IsValid)
            {
               var signInResult= await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.rememberMe, false);
                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(model.ReturnURl))
                    {
                        return Redirect(model.ReturnURl);
                    }
                    var user =await _userManager.FindByNameAsync(model.UserName);
                    var roles =await _userManager.GetRolesAsync(user);
                    //bu iş başarılı
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("AdminPanel");
                    }
                    return RedirectToAction("Panel");
                }
                else
                {
                    ModelState.AddModelError(" ", "Kullanıcı Adı veya Şifre Hatalı");
                }

            }
            
            return View(model);
        }
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var userName = User.Identity.Name;
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
                 
            return View();
        }
        [Authorize(Roles ="Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
        [Authorize(Roles ="Member")]
        public IActionResult Panel()
        {
            return View();
        }

        [Authorize(Roles ="Member")]
        public IActionResult MemberPage()
        {
            return View();
        }
        public async Task<IActionResult> MySignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
