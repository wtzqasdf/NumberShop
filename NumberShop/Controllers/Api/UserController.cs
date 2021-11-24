using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.RestParams.User;
using NumberShop.Models.Tables;
using NumberShop.Models.Repository;
using NumberShop.Models.Contexts;
using NumberShop.Commons;
using NumberShop.Filters;
using NumberShop.Commons.Cookie;

namespace NumberShop.Controllers.Api
{
    [Route("api/[controller]")]
    public class UserController : BaseApiController
    {
        UserRepository _userRepo;
        SessionRepository _sessionRepo;
        CookieWapper _cookie;

        public UserController(
            CookieWapper cookie,
            UserDbContext userDbContext,
            SessionDbContext sessionDbContext)
        {
            _cookie = cookie;
            _userRepo = new UserRepository(userDbContext);
            _sessionRepo = new SessionRepository(sessionDbContext, userDbContext);
        }

        [Route("login")]
        [HttpPost]
        [Guest]
        [ModelAuth]
        public IActionResult Login([FromBody]LoginInfo data)
        {
            User user = _userRepo.GetUser(data.Account);
            if (user == null || !Crypter.Verify(data.Password, user.Password))
            {
                return Json(false, "帳號或密碼錯誤");
            }
            string token = Guid.NewGuid().ToString();
            _cookie.Token = token;
            _sessionRepo.AddSession(user.Account, token);
            return Json(true);
        }

        [Route("register")]
        [HttpPost]
        [Guest]
        [ModelAuth]
        public IActionResult Register([FromBody]RegisterInfo data)
        {
            _userRepo.AddUser(data);
            return Json(true);
        }

        [Route("logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            _cookie.Token = null;
            return Json(true);
        }

        /// <summary>
        /// 取得會員資料
        /// </summary>
        [Route("member")]
        [HttpGet]
        [Member]
        public IActionResult Member()
        {
            User user = _sessionRepo.GetSessionAsUser(_cookie.Token);
            return Json(true, new { user = user });
        }

        /// <summary>
        /// 修改會員資料
        /// </summary>
        [Route("member")]
        [HttpPut]
        [Member]
        [ModelAuth]
        public IActionResult Member([FromBody]ProfileInfo info)
        {
            User user = _sessionRepo.GetSessionAsUser(_cookie.Token);
            //如果其中一個密碼有輸入的話
            if (!string.IsNullOrWhiteSpace(info.OldPassword) ||
                !string.IsNullOrWhiteSpace(info.NewPassword))
            {
                //驗證舊密碼
                if (!Crypter.Verify(info.OldPassword, user.Password))
                {
                    return Json(false, "舊密碼錯誤");
                }
            }
            _userRepo.UpdateUser(user.Account, info);
            return Json(true, "更新成功");
        }
    }
}
