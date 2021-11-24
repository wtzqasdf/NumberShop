using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.Tables.External;
using NumberShop.Models.Tables;
using NumberShop.Models.Repository;
using NumberShop.Models.Contexts;
using NumberShop.Commons.Cookie;
using NumberShop.Filters;

namespace NumberShop.Controllers.Api
{
    [Route("api/[controller]")]
    public class CouponController : BaseApiController
    {
        CouponRepository _couponRepo;
        UserCouponRepository _userCouponRepo;
        SessionRepository _sessionRepo;
        CookieWapper _cookie;

        public CouponController(
            CouponDbContext couponDbContext,
            UserCouponDbContext userCouponDbContext,
            SessionDbContext sessionContext,
            UserDbContext userContext,
            CookieWapper cookie)
        {
            _couponRepo = new CouponRepository(couponDbContext);
            _userCouponRepo = new UserCouponRepository(userCouponDbContext, couponDbContext);
            _sessionRepo = new SessionRepository(sessionContext, userContext);
            _cookie = cookie;
        }

        [HttpGet]
        [Member]
        public IActionResult Index()
        {
            Coupon[] coupons = _couponRepo.GetCoupons();
            return Json(true, new { coupons = coupons });
        }

        [Route("{couponID}")]
        [HttpPost]
        [Member]
        public IActionResult Index([FromRoute]string couponID)
        {
            User user = _sessionRepo.GetSessionAsUser(_cookie.Token);
            if (!_couponRepo.CouponIsEnough(couponID))
            {
                return Json(false, "此優惠券已領用完畢");
            }
            if (_userCouponRepo.Exists(user, couponID))
            {
                return Json(false, "您已拿過此優惠券");
            }
            _userCouponRepo.AddUserCoupon(user, couponID);
            _couponRepo.UpdateCouponRemainCount(couponID, -1);
            return Json(true, "優惠券取得成功");
        }

        [Route("user")]
        [HttpGet]
        [Member]
        public IActionResult SingleUser() 
        {
            User user = _sessionRepo.GetSessionAsUser(_cookie.Token);
            Coupon[] coupons = _userCouponRepo.GetNotUseCoupons(user);
            return Json(true, new { coupons = coupons });
        }
    }
}
