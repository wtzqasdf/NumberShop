using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NumberShop.Models.RestParams.User
{
    public class ProfileInfo
    {
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        public string OldPassword { get; set; }
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        public string NewPassword { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(maximumLength: 255, MinimumLength = 1)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 1)]
        public string Identity { get; set; }
    }
}
