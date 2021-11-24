using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NumberShop.Models.Tables
{
    public class MerchOrder
    {
        public string OrderID { get; set; }
        public string Account { get; set; }
        public string MerchID { get; set; }
        public string MerchName { get; set; }
        public int Count { get; set; }
        public decimal OriginPrice { get; set; }
        [JsonIgnore]
        public string CouponID { get; set; }
        [JsonIgnore]
        public DateTime OrderTime { get; set; }
        [NotMapped]
        public string OrderTimeText { get { return OrderTime.ToString("yyyy-MM-dd HH:mm:ss"); } }
    }
}
