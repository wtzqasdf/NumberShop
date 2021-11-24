using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberShop.Models.RestParams.Merch
{
    public class Review_C
    {
        public string MerchID { get; set; }
        public int Score { get; set; }
        public string Content { get; set; }
    }
}
