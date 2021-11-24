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
    public class UserCouponRepository : IDisposable
    {
        UserCouponDbContext _userCouponDbContext;
        CouponDbContext _couponDbContext;

        public UserCouponRepository(UserCouponDbContext userCouponDbContext, CouponDbContext couponDbContext)
        {
            _userCouponDbContext = userCouponDbContext;
            _couponDbContext = couponDbContext;
        }

        public bool Exists(User user, string couponID)
        {
            UserCoupon coupon = (from t in _userCouponDbContext.UserCoupons where t.Account == user.Account && t.CouponID == couponID select t).ToArray().FirstOrDefault();
            return coupon != null;
        }

        public void AddUserCoupon(User user, string couponID)
        {
            UserCoupon coupon = new UserCoupon();
            coupon.Account = user.Account;
            coupon.CouponID = couponID;
            coupon.GotDate = DateTime.Now;
            coupon.IsUsed = false;
            _userCouponDbContext.UserCoupons.Add(coupon);
            _userCouponDbContext.SaveChanges();
        }

        public Coupon[] GetNotUseCoupons(User user)
        {
            UserCoupon[] userCoupons = (from t in _userCouponDbContext.UserCoupons where t.Account == user.Account && t.IsUsed == false select t).AsNoTracking().ToArray();
            Coupon[] coupons = (from t in userCoupons join a in _couponDbContext.Coupons on t.CouponID equals a.CouponID select a).ToArray();
            return coupons;
        }

        public void UpdateAsUsed(User user, string couponID)
        {
            UserCoupon userCoupon = (from t in _userCouponDbContext.UserCoupons where t.Account == user.Account && t.CouponID == couponID select t).ToArray().FirstOrDefault();
            if (userCoupon != null)
            {
                userCoupon.IsUsed = true;
                _userCouponDbContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            _userCouponDbContext = null;
        }
    }
}
