using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberShop.Models.RestParams.User
{
    public class LoginInfo
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        public string Account { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
