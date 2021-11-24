using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.Contexts;
using NumberShop.Models.Tables;
using NumberShop.Models.Tables.External;
using Microsoft.EntityFrameworkCore;

namespace NumberShop.Models.Repository
{
    public class MerchOrderRepository : IDisposable
    {
        MerchOrderDbContext _merchOrderContext;
        CouponDbContext _couponContext;
        public MerchOrderRepository(MerchOrderDbContext merchOrderContext, CouponDbContext couponContext)
        {
            _merchOrderContext = merchOrderContext;
            _couponContext = couponContext;
        }

        public MerchOrder[] CreateOrderFromCart(User user, CartMerch[] carts, string couponID = "")
        {
            //Create a new order to databse
            MerchOrder[] orders = new MerchOrder[carts.Length];
            string orderID = Guid.NewGuid().ToString();
            DateTime orderTime = DateTime.Now;
            for (int i = 0; i < orders.Length; i++)
            {
                orders[i] = new MerchOrder();
                orders[i].OrderID = orderID;
                orders[i].OrderTime = orderTime;
                orders[i].Account = user.Account;
                orders[i].Count = carts[i].Count;
                orders[i].OriginPrice = carts[i].Price;
                orders[i].CouponID = couponID;
                orders[i].MerchID = carts[i].MerchID;
                orders[i].MerchName = carts[i].Name;
            }
            _merchOrderContext.Orders.AddRange(orders);
            _merchOrderContext.SaveChanges();
            return orders;
        }

        public MerchOrderContainer[] GetOrdersFromUser(User user)
        {
            MerchOrder[] orders = (from t in _merchOrderContext.Orders where t.Account == user.Account orderby t.OrderTime descending select t).AsNoTracking().ToArray();
            MerchOrderContainer[] containers = (from t in orders group t by t.OrderID into z select new MerchOrderContainer(z.ToArray(), z.Key)).ToArray();
            foreach (MerchOrderContainer c in containers)
            {
                MerchOrder order = c.Orders.FirstOrDefault();
                Coupon coupon = (from t in _couponContext.Coupons where t.CouponID == order.CouponID select t).ToArray().FirstOrDefault();
                if (coupon != null)
                {
                    if (coupon.Percent != null)
                    {
                        c.TotalPrice = (int)(c.TotalPrice * ((decimal)coupon.Percent / 100));
                    }
                    else
                    {
                        c.TotalPrice -= (int)coupon.Price;
                    }
                    c.HasUseCoupon = true;
                }
                else
                {
                    c.HasUseCoupon = false;
                }
            }
            return containers;
        }

        public void Dispose()
        {
            _merchOrderContext = null;
        }
    }
}
