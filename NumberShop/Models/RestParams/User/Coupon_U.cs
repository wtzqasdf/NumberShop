using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberShop.Models.RestParams.User
{
    public class Coupon_U
    {
        [Required]
        public string CouponID { get; set; }
        [Required]
        public string CouponName { get; set; }
        [Required]
        [RegularExpression("^(percent|price)$")]
        public string CouponType { get; set; }
        [Required]
        [Range(1, 999999)]
        public decimal CouponPP { get; set; }
        [Required]
        [Range(1, 99999)]
        public int RemainCount { get; set; }
        [Required]
        public DateTime CouponExpireDate { get; set; }
    }
}
