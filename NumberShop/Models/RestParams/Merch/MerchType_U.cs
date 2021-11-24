using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberShop.Models.RestParams.Merch
{
    public class MerchType_U
    {
        [Required]
        public string TypeID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
