using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NumberShop.Models.Tables
{
    public class UserCoupon
    {
        public string Account { get; set; }
        public string CouponID { get; set; }
        public bool IsUsed { get; set; }
        public DateTime GotDate { get; set; }
    }
}
