using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NumberShop.Models.Contexts;
using NumberShop.Models.Tables;

namespace NumberShop.Filters
{
    public class Manager : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            SessionDbContext sessionContext = context.HttpContext.RequestServices.GetService(typeof(SessionDbContext)) as SessionDbContext;
            UserDbContext userContext = context.HttpContext.RequestServices.GetService(typeof(UserDbContext)) as UserDbContext;
            string message = "";
            if (context.HttpContext.Request.Cookies.TryGetValue("token", out string token))
            {
                //if token of cookie has login record, It is denied. 
                Session s = sessionContext.Sessions.Where(w => w.Token == token).ToArray().FirstOrDefault();
                if (s != null)
                {
                    //check permission
                    User user = userContext.Users.Where(w => w.Account == s.Account).ToArray().FirstOrDefault();
                    if (user.Permission == 0)
                    {
                        message = "權限不足";
                    }
                }
                else
                {
                    message = "請先登入";
                }
            }
            else
            {
                //if client haven't token of cookie, It is denied.
                message = "請先登入";
            }
            if (!string.IsNullOrWhiteSpace(message))
            {
                context.Result = new JsonResult(new
                {
                    status = false,
                    messages = new string[] { message }
                });
            }
        }
    }
}
