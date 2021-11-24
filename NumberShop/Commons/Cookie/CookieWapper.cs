using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace NumberShop.Commons.Cookie
{
    public class CookieWapper
    {
        IHttpContextAccessor _accessor;
        public CookieWapper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        IRequestCookieCollection RequestCookies { get { return _accessor.HttpContext.Request.Cookies; } }
        IResponseCookies ResponseCookies { get { return _accessor.HttpContext.Response.Cookies; } }
        /// <summary>
        /// Get token
        /// </summary>
        public string Token
        {
            get
            {
                bool isSuccess = RequestCookies.TryGetValue("token", out string token);
                return isSuccess ? token : "";
            }
            set 
            {
                if (value == null)
                {
                    ResponseCookies.Delete("token");
                }
                else
                {
                    ResponseCookies.Append("token", value, new CookieOptions() {
                        Domain = "localhost",
                        HttpOnly = true,
                        IsEssential = true,
                        Path = "/",
                        SameSite = SameSiteMode.Lax
                    });
                }
            }
        }
    }
}
