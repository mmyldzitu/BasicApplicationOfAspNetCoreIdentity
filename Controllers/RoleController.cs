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
    [Authorize(Roles="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();

            return View(roles);
        }
        public IActionResult Create()
        {
            return View(new RoleCreateModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new AppRole()
                {

                    Name = model.Name,
                    CreatedTime = DateTime.Now
                }); 
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");

                }
                foreach ( var error in result.Errors)
                {
                    ModelState.AddModelError(" ", error.Description);
                }

            }
            return View(model);
        }
    }
}
