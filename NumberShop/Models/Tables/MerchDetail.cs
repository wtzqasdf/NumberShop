using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberShop.Models.Tables
{
    public class MerchDetail
    {
        public string MerchID { get; set; }
        public string TypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Remain { get; set; }
    }
}
