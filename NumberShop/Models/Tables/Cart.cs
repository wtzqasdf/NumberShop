using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberShop.Models.Tables
{
    public class Cart
    {
        public string Account { get; set; }
        public string MerchID { get; set; }
        public int Count { get; set; }
    }
}
