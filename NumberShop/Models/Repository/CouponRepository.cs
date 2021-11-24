using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.Contexts;
using NumberShop.Models.Tables;
using NumberShop.Models.RestParams.User;
using Microsoft.EntityFrameworkCore;

namespace NumberShop.Models.Repository
{
    public class CouponRepository : IDisposable
    {
        CouponDbContext _couponDbContext;

        public CouponRepository(CouponDbContext couponDbContext)
        {
            _couponDbContext = couponDbContext;
        }

        public void AddCoupon(Coupon_C model)
        {
            Coupon coupon = new Coupon();
            coupon.CouponID = Guid.NewGuid().ToString().Substring(0, 6);
            coupon.CouponName = model.CouponName;
            coupon.ExpireDate = model.CouponExpireDate;
            coupon.CouponType = model.CouponType;
            coupon.RemainCount = model.RemainCount;
            if (model.CouponType == "percent")
            {
                coupon.Percent = model.CouponPP;
                coupon.Price = null;
            }
            else
            {
                coupon.Percent = null;
                coupon.Price = model.CouponPP;
            }
            _couponDbContext.Coupons.Add(coupon);
            _couponDbContext.SaveChanges();
        }

        public Coupon[] GetCoupons()
        {
            return (from t in _couponDbContext.Coupons select t).ToArray();
        }

        public void UpdateCoupon(Coupon_U model)
        {
            Coupon coupon = (from t in _couponDbContext.Coupons where t.CouponID == model.CouponID select t).ToArray().FirstOrDefault();
            if (coupon != null)
            {
                coupon.CouponName = model.CouponName;
                coupon.CouponType = model.CouponType;
                coupon.ExpireDate = model.CouponExpireDate;
                coupon.RemainCount = model.RemainCount;
                if (model.CouponType == "percent")
                {
                    coupon.Percent = model.CouponPP;
                    coupon.Price = null;
                }
                else
                {
                    coupon.Percent = null;
                    coupon.Price = model.CouponPP;
                }
                _couponDbContext.SaveChanges();
            }
        }

        public void UpdateCouponRemainCount(string couponID, int count)
        {
            Coupon coupon = (from t in _couponDbContext.Coupons where t.CouponID == couponID select t).ToArray().FirstOrDefault();
            if (coupon != null)
            {
                coupon.RemainCount += count;
                _couponDbContext.SaveChanges();
            }
        }

        public bool CouponIsEnough(string couponID)
        {
            Coupon coupon = (from t in _couponDbContext.Coupons where t.CouponID == couponID select t).ToArray().FirstOrDefault();
            return coupon != null && coupon.RemainCount > 0;
        }
        public void DeleteCoupon(string couponID) 
        {
            Coupon coupon = (from t in _couponDbContext.Coupons where t.CouponID == couponID select t).ToArray().FirstOrDefault();
            if (coupon != null)
            {
                _couponDbContext.Coupons.Remove(coupon);
                _couponDbContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            _couponDbContext = null;
        }
    }
}
