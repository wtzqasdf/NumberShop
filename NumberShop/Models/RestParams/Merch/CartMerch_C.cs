using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberShop.Models.RestParams.Merch
{
    public class CartMerch_C
    {
        [Required]
        public string MerchID { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
