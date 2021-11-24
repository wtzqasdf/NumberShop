using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace NumberShop.Models.RestParams.User
{
    public class RegisterInfo
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        public string Account { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(maximumLength: 255, MinimumLength = 1)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 1)]
        public string Identity { get; set; }
    }
}
