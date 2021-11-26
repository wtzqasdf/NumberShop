using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberShop.Models.RestParams.Merch
{
    public class Review_C
    {
        [Required]
        public string MerchID { get; set; }
        [Required]
        [Range(1, 5)]
        public int Score { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Content { get; set; }
    }
}
