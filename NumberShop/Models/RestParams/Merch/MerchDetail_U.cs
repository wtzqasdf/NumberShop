using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberShop.Models.RestParams.Merch
{
    public class MerchDetail_U
    {
        [Required]
        public string MerchID { get; set; }
        [Required]
        public string TypeID { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Description { get; set; }
        [Required]
        [Range(1, 1000000)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 999999)]
        public int Remain { get; set; }
    }
}
