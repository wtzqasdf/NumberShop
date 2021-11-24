using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberShop.Models.Tables.External
{
    public class MerchOrderContainer
    {
        public MerchOrder[] Orders { get; set; }
        public string OrderID { get; set; }
        public int TotalPrice { get; set; }
        public bool HasUseCoupon { get; set; }

        public MerchOrderContainer(MerchOrder[] orders, string orderID)
        {
            Orders = orders;
            OrderID = orderID;
            foreach (MerchOrder o in orders)
            {
                TotalPrice += (int)(o.OriginPrice * o.Count);
            }
        }
    }
}
