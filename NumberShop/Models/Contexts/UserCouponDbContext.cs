using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using NumberShop.Models.Tables;

namespace NumberShop.Models.Contexts
{
    public class UserCouponDbContext : DbContext
    {
        public DbSet<UserCoupon> UserCoupons { get; set; }
        public UserCouponDbContext(DbContextOptions<UserCouponDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCoupon>().ToTable("usercoupon").HasKey(k => new { k.Account, k.CouponID });
        }
    }
}
