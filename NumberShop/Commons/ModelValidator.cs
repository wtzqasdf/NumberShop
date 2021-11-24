using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace NumberShop.Commons
{
    public class ModelValidator
    {
        IActionContextAccessor _accessor;
        public ModelValidator(IActionContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public bool IsValid { get { return _accessor.ActionContext.ModelState.IsValid; } }

        public string[] GetErrors()
        {
            List<string> errors = new List<string>();
            foreach (var val in _accessor.ActionContext.ModelState.Values)
            {
                foreach (var error in val.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors.ToArray();
        }
    }
}
