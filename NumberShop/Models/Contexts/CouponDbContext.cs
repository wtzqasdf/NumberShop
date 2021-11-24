using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using NumberShop.Models.Tables;

namespace NumberShop.Models.Contexts
{
    public class CouponDbContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; }
        public CouponDbContext(DbContextOptions<CouponDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().ToTable("coupon").HasKey(k => k.CouponID);
        }
    }
}
