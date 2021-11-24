using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.RestParams.Merch;
using NumberShop.Models.Tables;
using NumberShop.Models.Repository;
using NumberShop.Models.Contexts;
using NumberShop.Commons.Cookie;
using NumberShop.Filters;

namespace NumberShop.Controllers.Api
{
    [Route("api/[controller]")]
    public class CartController : BaseApiController
    {
        CartRepository _cartRepo;
        SessionRepository _sessionRepo;
        CookieWapper _cookie;

        public CartController(
            SessionDbContext sessionDbContext,
            UserDbContext userDbContext,
            CartDbContext cartDbContext,
            MerchDetailDbContext merchDetailContext,
            CookieWapper cookie)
        {
            _sessionRepo = new SessionRepository(sessionDbContext, userDbContext);
            _cartRepo = new CartRepository(cartDbContext, merchDetailContext);
            _cookie = cookie;
        }

        [Route("merch")]
        [HttpGet]
        [Member]
        public IActionResult Merch()
        {
            var results = _cartRepo.GetCartMerches(_sessionRepo.GetSessionAsUser(_cookie.Token));
            return Json(true, new { bill = results.bill, merches = results.merches });
        }

        [Route("merch")]
        [HttpPost]
        [Member]
        [ModelAuth]
        public IActionResult Merch([FromBody]CartMerch_C model)
        {
            _cartRepo.AddToCart(_sessionRepo.GetSessionAsUser(_cookie.Token), model);
            return Json(true, "已加入到購物車");
        }

        [Route("merch")]
        [HttpDelete]
        [Member]
        [ModelAuth]
        public IActionResult Merch([FromBody]CartMerch_D model)
        {
            _cartRepo.DeleteCartMerch(_sessionRepo.GetSessionAsUser(_cookie.Token), model.MerchID);
            return Json(true, "刪除成功");
        }
    }
}
