using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.Contexts;
using NumberShop.Models.Tables;

namespace NumberShop.Models.Repository
{
    public class SessionRepository : IDisposable
    {
        SessionDbContext _sessionDbContext;
        UserDbContext _userDbContext;

        public SessionRepository(SessionDbContext sessionDbContext, UserDbContext userDbContext)
        {
            _sessionDbContext = sessionDbContext;
            _userDbContext = userDbContext;
        }

        public void AddSession(string account, string token)
        {
            Session s = new Session();
            s.Account = account;
            s.Token = token;
            _sessionDbContext.Sessions.Add(s);
            _sessionDbContext.SaveChanges();
        }

        public bool Exists(string token)
        {
            int count = (from t in _sessionDbContext.Sessions where t.Token == token select t).ToArray().Length;
            return count != 0;
        }

        public User GetSessionAsUser(string token)
        {
            Session s = (from t in _sessionDbContext.Sessions where t.Token == token select t).ToArray().FirstOrDefault();
            User user = (from t in _userDbContext.Users where t.Account == s.Account select t).ToArray().FirstOrDefault();
            return user;
        }

        public void Dispose()
        {
            _sessionDbContext = null;
            _userDbContext = null;
        }
    }
}
