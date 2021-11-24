using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberShop.Controllers.Api
{
    public class BaseApiController : Controller
    {
        protected IActionResult Json(bool status)
        {
            return Json(new { Status = status });
        }

        protected IActionResult Json(bool status, params string[] messages)
        {
            return Json(new { Status = status, Messages = messages });
        }

        protected IActionResult Json(bool status, object inputs)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("status", status);
            var props = inputs.GetType().GetProperties();
            foreach (var p in props)
            {
                dic.Add(p.Name, p.GetValue(inputs));
            }
            return Json(dic);
        }
    }
}
