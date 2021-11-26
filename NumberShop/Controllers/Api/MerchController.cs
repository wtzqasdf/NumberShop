using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.Tables;
using NumberShop.Models.Repository;
using NumberShop.Models.Contexts;
using NumberShop.Models.RestParams.Merch;
using NumberShop.Commons.Cookie;
using NumberShop.Filters;

namespace NumberShop.Controllers.Api
{
    [Route("api/[controller]")]
    public class MerchController : BaseApiController
    {
        MerchTypeRepository _merchTypeRepo;
        MerchDetailRepository _merchDetailRepo;
        ReviewRepository _reviewRepo;
        SessionRepository _sessionRepo;
        CookieWapper _cookie;

        public MerchController(
            MerchTypeDbContext merchTypeDbContext,
            MerchDetailDbContext merchDetailDbContext,
            ReviewDbContext reviewContext,
            SessionDbContext sessionContext,
            UserDbContext userContext,
            CookieWapper cookie)
        {
            _merchTypeRepo = new MerchTypeRepository(merchTypeDbContext);
            _merchDetailRepo = new MerchDetailRepository(merchDetailDbContext);
            _reviewRepo = new ReviewRepository(reviewContext);
            _sessionRepo = new SessionRepository(sessionContext, userContext);
            _cookie = cookie;
        }

        [Route("merchtype")]
        [HttpGet]
        public IActionResult MerchType()
        {
            MerchType[] types = _merchTypeRepo.GetMerchTypes();
            return Json(true, new { types = types });
        }

        [Route("merchdetail/{typeID}")]
        [HttpGet]
        public IActionResult MerchDetails([FromRoute]string typeID)
        {
            MerchDetail[] details = _merchDetailRepo.GetMerchDetails(typeID);
            return Json(true, new { details = details });
        }

        [Route("merchdetail/single/{merchID}")]
        [HttpGet]
        public IActionResult MerchDetail([FromRoute]string merchID)
        {
            MerchDetail detail = _merchDetailRepo.GetMerchDetail(merchID);
            return Json(true, new { detail = detail });
        }

        [Route("review/{merchID}")]
        [HttpGet]
        public IActionResult Review([FromRoute]string merchID)
        {
            Review[] reviews = _reviewRepo.GetReviews(merchID);
            return Json(true, new { reviews = reviews });
        }

        [Route("review")]
        [HttpPost]
        [ModelAuth]
        public IActionResult Review([FromBody]Review_C model)
        {
            User user = _sessionRepo.GetSessionAsUser(_cookie.Token);
            _reviewRepo.AddReview(user, model);
            return Json(true);
        }
    }
}
