using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.Contexts;
using NumberShop.Models.Tables;
using NumberShop.Models.RestParams.Merch;
using Microsoft.EntityFrameworkCore;

namespace NumberShop.Models.Repository
{
    public class ReviewRepository : IDisposable
    {
        ReviewDbContext _reviewContext;

        public ReviewRepository(ReviewDbContext reviewContext)
        {
            _reviewContext = reviewContext;
        }

        public void AddReview(User user, Review_C model)
        {
            Review r = new Review();
            r.Account = user.Account;
            r.MerchID = model.MerchID;
            r.Content = model.Content;
            r.Score = model.Score;
            r.PostDate = DateTime.Now;
            _reviewContext.Reviews.Add(r);
            _reviewContext.SaveChanges();
        }

        public Review[] GetReviews(string merchID)
        {
            return (from t in _reviewContext.Reviews where t.MerchID == merchID select t).AsNoTracking().ToArray();
        }

        public void Dispose()
        {
            _reviewContext = null;
        }
    }
}
