using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberShop.Models.Tables.External
{
    public class CartBill
    {
        public decimal TotalPrice { get; set; }
        public int MerchCount { get; set; }
    }
}
