﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberShop.Models.RestParams.Merch
{
    public class Order_C
    {
        [Required]
        public string CouponID { get; set; }
    }
}
