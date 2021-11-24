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
    public class Member : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            SessionDbContext sessionContext = context.HttpContext.RequestServices.GetService(typeof(SessionDbContext)) as SessionDbContext;
            bool isInvalid = false;
            if (context.HttpContext.Request.Cookies.TryGetValue("token", out string token))
            {
                //if token of cookie has login record, It is denied. 
                Session[] s = sessionContext.Sessions.Where(w => w.Token == token).ToArray();
                if (s.Length == 0)
                {
                    isInvalid = true;
                }
            }
            else
            {
                //if client haven't token of cookie, It is denied.
                isInvalid = true;
            }
            if (isInvalid)
            {
                context.Result = new JsonResult(new
                {
                    status = false,
                    messages = new string[] { "請先登入" }
                });
            }
        }
    }
}
