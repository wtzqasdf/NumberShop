using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using NumberShop.Models.Tables;

namespace NumberShop.Models.Contexts
{
    public class ReviewDbContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public ReviewDbContext(DbContextOptions<ReviewDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().ToTable("review").HasKey(k => new { k.MerchID });
        }
    }
}
