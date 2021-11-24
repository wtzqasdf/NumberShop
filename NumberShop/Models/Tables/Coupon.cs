using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NumberShop.Models.Tables
{
    public class Coupon
    {
        public string CouponID { get; set; }
        public string CouponName { get; set; }
        public string CouponType { get; set; }
        public decimal? Percent { get; set; }
        public decimal? Price { get; set; }
        public int RemainCount { get; set; }
        [JsonIgnore]
        public DateTime ExpireDate { get; set; }
        public string ExpireDateText { get { return ExpireDate.ToString("yyyy-MM-dd"); } }
    }
}
