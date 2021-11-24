using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberShop.Models.RestParams.Merch
{
    public class CartMerch_D
    {
        [Required]
        public string MerchID { get; set; }
    }
}
