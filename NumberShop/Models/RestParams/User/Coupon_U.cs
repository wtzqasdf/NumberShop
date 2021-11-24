using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberShop.Models.RestParams.User
{
    public class Coupon_U
    {
        public string CouponID { get; set; }
        public string CouponName { get; set; }
        public string CouponType { get; set; }
        public decimal CouponPP { get; set; }
        public int RemainCount { get; set; }
        public DateTime CouponExpireDate { get; set; }
    }
}
