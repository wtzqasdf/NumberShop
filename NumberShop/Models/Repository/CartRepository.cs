using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.Contexts;
using NumberShop.Models.Tables;
using NumberShop.Models.RestParams.Merch;
using NumberShop.Models.Tables.External;
using Microsoft.EntityFrameworkCore;

namespace NumberShop.Models.Repository
{
    public class CartRepository
    {
        CartDbContext _cartDbContext;
        MerchDetailDbContext _merchDetailContext;

        public CartRepository(CartDbContext cartDbContext, MerchDetailDbContext merchDetailContext)
        {
            _cartDbContext = cartDbContext;
            _merchDetailContext = merchDetailContext;
        }

        public void AddToCart(User user, CartMerch_C model)
        {
            Cart source = (from t in _cartDbContext.Carts where t.Account == user.Account && t.MerchID == model.MerchID select t).ToArray().FirstOrDefault();
            if (source != null)
            {
                source.Count = model.Count;
            }
            else
            {
                Cart c = new Cart();
                c.Account = user.Account;
                c.Count = model.Count;
                c.MerchID = model.MerchID;
                _cartDbContext.Carts.Add(c);
            }
            _cartDbContext.SaveChanges();
        }

        public (CartBill bill, CartMerch[] merches) GetCartMerches(User user)
        {
            Cart[] carts = (from t in _cartDbContext.Carts where t.Account == user.Account select t).AsNoTracking().ToArray();
            MerchDetail[] merchDetails = (from t1 in carts join t2 in _merchDetailContext.MerchDetails on t1.MerchID equals t2.MerchID select t2).ToArray();

            CartBill cartBill = new CartBill();
            List<CartMerch> cartMerches = new List<CartMerch>();
            for (int i = 0; i < carts.Length; i++)
            {
                MerchDetail detail = merchDetails[i];
                Cart cart = carts.Where(c => c.MerchID == detail.MerchID).FirstOrDefault();
                cartBill.MerchCount += cart.Count;
                cartBill.TotalPrice += detail.Price * cart.Count;

                CartMerch cartMerch = new CartMerch();
                cartMerch.MerchID = detail.MerchID;
                cartMerch.Name = detail.Name;
                cartMerch.Price = detail.Price;
                cartMerch.Count = cart.Count;
                cartMerches.Add(cartMerch);
            }

            return (cartBill, cartMerches.ToArray());
        }

        public void DeleteCartMerch(User user, string merchID)
        {
            Cart cart = (from t in _cartDbContext.Carts where t.Account == user.Account && t.MerchID == merchID select t).ToArray().FirstOrDefault();
            if (cart != null)
            {
                _cartDbContext.Carts.Remove(cart);
                _cartDbContext.SaveChanges();
            }
        }

        public void DeleteCartMerches(User user)
        {
            Cart[] carts = (from t in _cartDbContext.Carts where t.Account == user.Account select t).ToArray();
            _cartDbContext.Carts.RemoveRange(carts);
            _cartDbContext.SaveChanges();
        }
    }
}
