using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.Tables;
using NumberShop.Models.Repository;
using NumberShop.Models.Contexts;
using NumberShop.Models.RestParams.Merch;
using NumberShop.Models.Tables.External;
using NumberShop.Commons.Cookie;
using NumberShop.Filters;

namespace NumberShop.Controllers.Api
{
    [Route("api/[controller]")]
    public class OrderController : BaseApiController
    {
        MerchOrderRepository _merchOrderRepo;
        MerchDetailRepository _merchDetailRepo;
        CartRepository _cartRepo;
        UserCouponRepository _userCouponRepo;
        SessionRepository _sessionRepo;
        CookieWapper _cookie;

        public OrderController(
            MerchOrderDbContext merchOrderContext,
            MerchDetailDbContext merchDetailContext,
            CartDbContext cartDbContext,
            SessionDbContext sessionContext,
            UserCouponDbContext userCouponContext,
            CouponDbContext couponContext,
            UserDbContext userContext,
            CookieWapper cookie)
        {
            _merchOrderRepo = new MerchOrderRepository(merchOrderContext, couponContext);
            _merchDetailRepo = new MerchDetailRepository(merchDetailContext);
            _cartRepo = new CartRepository(cartDbContext, merchDetailContext);
            _userCouponRepo = new UserCouponRepository(userCouponContext, couponContext);
            _sessionRepo = new SessionRepository(sessionContext, userContext);
            _cookie = cookie;
        }

        [Route("merch")]
        [HttpGet]
        [Member]
        public IActionResult MerchAll()
        {
            User user = _sessionRepo.GetSessionAsUser(_cookie.Token);
            MerchOrderContainer[] containers = _merchOrderRepo.GetOrdersFromUser(user);
            return Json(true, new { containers = containers });
        }

        [Route("merch")]
        [HttpPost]
        [Member]
        public IActionResult Merch([FromBody]Order_C model)
        {
            //1, get user from cookie token
            User user = _sessionRepo.GetSessionAsUser(_cookie.Token);
            //2, get cart datas from user account
            var cartResults = _cartRepo.GetCartMerches(user);
            if (cartResults.merches.Length == 0)
            {
                return Json(false, "您的購物車沒有任何商品呢");
            }
            if (!_merchDetailRepo.CheckMerchCountIsEnough(cartResults.merches, out string[] messages))
            {
                return Json(false, messages);
            }
            //3, create a order from cart datas
            MerchOrder[] orders = _merchOrderRepo.CreateOrderFromCart(user, cartResults.merches, model.CouponID);
            //4, set coupon as used
            _userCouponRepo.UpdateAsUsed(user, model.CouponID);
            //5, delete all cart merches of user
            _cartRepo.DeleteCartMerches(user);
            //6, update and reduce count for every merch
            _merchDetailRepo.ReduceMerchesCount(orders);
            return Json(true);
        }
    }
}
