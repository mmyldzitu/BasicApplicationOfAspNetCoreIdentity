using AspNetCoreIdentity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.TagHelpers
{
    [HtmlTargetElement("getUserInfo")]
    public class GetUserInfo : TagHelper
    {
        private readonly UserManager<AppUser> _userManager;
        public int UserId { get; set; }
        public GetUserInfo(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var html = "";
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == UserId);
            var roles = await _userManager.GetRolesAsync(user);
            var rolescount = roles.Count();

            if (rolescount > 1)
            {


                for (int i = 0; i < rolescount; i++)
                {
                    if (rolescount == i + 1)
                    {
                        html += roles[i];

                    }
                    else
                    {
                        html += roles[i] + ", ";

                    }
                }
            }

            else
            {
                for (int i = 0; i < rolescount; i++)
                {

                    html += roles[i];
                }

            }






            output.Content.SetHtmlContent(html);
        }

    }
}
