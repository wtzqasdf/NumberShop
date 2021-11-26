using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberShop.Models.RestParams.Merch
{
    public class MerchType_C
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
