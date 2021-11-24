using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.RestParams.Merch;
using NumberShop.Models.Tables;
using NumberShop.Models.Repository;
using NumberShop.Models.Contexts;
using NumberShop.Models.RestParams.User;
using NumberShop.Filters;

namespace NumberShop.Controllers.Api.Management
{
    [Route("api/m/[controller]")]
    public class CouponController : BaseApiController
    {
        CouponRepository _couponRepo;
        public CouponController(CouponDbContext couponDbContext)
        {
            _couponRepo = new CouponRepository(couponDbContext);
        }

        [HttpGet]
        [Manager]
        public IActionResult Index()
        {
            Coupon[] coupons = _couponRepo.GetCoupons();
            return Json(true, new { coupons = coupons });
        }

        [HttpPost]
        [Manager]
        public IActionResult Index([FromBody] Coupon_C model)
        {
            _couponRepo.AddCoupon(model);
            return Json(true, "新增成功");
        }

        [HttpPut]
        [Manager]
        public IActionResult Index([FromBody] Coupon_U model)
        {
            _couponRepo.UpdateCoupon(model);
            return Json(true, "儲存成功");
        }

        [Route("{couponID}")]
        [HttpDelete]
        [Manager]
        public IActionResult Index([FromRoute]string couponID)
        {
            _couponRepo.DeleteCoupon(couponID);
            return Json(true, "刪除成功");
        }
    }
}
