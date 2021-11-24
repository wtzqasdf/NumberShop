using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using NumberShop.Models.Contexts;
using NumberShop.Models.Tables;
using NumberShop.Models.RestParams.User;
using NumberShop.Commons;

namespace NumberShop.Models.Repository
{
    public class UserRepository : IDisposable
    {
        UserDbContext _userDbContext;
        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public void AddUser(RegisterInfo member)
        {
            User user = new User();
            user.Account = member.Account;
            user.Password = Crypter.Crypt(member.Password);
            user.Identity = member.Identity;
            user.Email = member.Email;
            user.Permission = 0;
            user.RegDate = DateTime.Now;
            _userDbContext.Users.Add(user);
            _userDbContext.SaveChanges();
        }

        public User GetUser(string account)
        {
            User user = null;
            user = (from t in _userDbContext.Users where t.Account == account select t).ToArray().FirstOrDefault();
            return user;
        }

        public void UpdateUser(string account, ProfileInfo info)
        {
            User user = (from t in _userDbContext.Users where t.Account == account select t).ToArray().FirstOrDefault();
            if (user != null)
            {
                user.Email = info.Email;
                user.Identity = info.Identity;
                if (!string.IsNullOrWhiteSpace(info.NewPassword))
                {
                    user.Password = Crypter.Crypt(info.NewPassword);
                }
                _userDbContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            _userDbContext = null;
        }
    }
}
