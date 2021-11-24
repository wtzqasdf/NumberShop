using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using NumberShop.Models.RestParams.Merch;
using NumberShop.Models.Tables;
using NumberShop.Models.Repository;
using NumberShop.Models.Contexts;
using NumberShop.Filters;

namespace NumberShop.Controllers.Api.Management
{
    [Route("api/m/[controller]")]
    public class MerchController : BaseApiController
    {
        MerchTypeRepository _merchTypeRepo;
        MerchDetailRepository _merchDetailRepo;
        public MerchController(
            MerchTypeDbContext merchTypeDbContext,
            MerchDetailDbContext merchDetailDbContext)
        {
            _merchTypeRepo = new MerchTypeRepository(merchTypeDbContext);
            _merchDetailRepo = new MerchDetailRepository(merchDetailDbContext);
        }

        [Route("merchtype")]
        [HttpGet]
        [Manager]
        public IActionResult MerchType()
        {
            MerchType[] types = _merchTypeRepo.GetMerchTypes();
            return Json(true, new { types = types });
        }

        [Route("merchtype")]
        [HttpPost]
        [Manager]
        [ModelAuth]
        public IActionResult MerchType([FromBody] MerchType_C model)
        {
            _merchTypeRepo.AddMerchType(model);
            return Json(true, "新增成功");
        }

        [Route("merchtype")]
        [HttpPut]
        [Manager]
        [ModelAuth]
        public IActionResult MerchType([FromBody] MerchType_U model)
        {
            _merchTypeRepo.UpdateMerchType(model);
            return Json(true, "更新成功");
        }

        [Route("merchtype")]
        [HttpDelete]
        [Manager]
        [ModelAuth]
        public IActionResult MerchType([FromBody] MerchType_D model)
        {
            _merchTypeRepo.DeleteMerchType(model);
            return Json(true, "刪除成功");
        }

        [Route("merchdetail")]
        [HttpGet]
        [Manager]
        public IActionResult MerchDetail()
        {
            MerchDetail[] details = _merchDetailRepo.GetMerchDetails();
            return Json(true, new { details = details });
        }

        [Route("merchdetail")]
        [HttpPost]
        [Manager]
        [ModelAuth]
        public IActionResult MerchDetail([FromBody]MerchDetail_C model)
        {
            _merchDetailRepo.AddMerchDetail(model);
            return Json(true, "新增成功");
        }

        [Route("merchdetail")]
        [HttpPut]
        [Manager]
        [ModelAuth]
        public IActionResult MerchDetail([FromBody]MerchDetail_U model)
        {
            _merchDetailRepo.UpdateMerchDetail(model);
            return Json(true, "更新成功");
        }

        [Route("merchdetail")]
        [HttpDelete]
        [Manager]
        [ModelAuth]
        public IActionResult MerchDetail([FromBody]MerchDetail_D model)
        {
            _merchDetailRepo.DeleteMerchDetail(model);
            return Json(true, "刪除成功");
        }
    }
}
