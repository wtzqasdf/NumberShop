using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using NumberShop.Models.Tables;

namespace NumberShop.Models.Contexts
{
    public class MerchDetailDbContext : DbContext
    {
        public DbSet<MerchDetail> MerchDetails { get; set; }
        public MerchDetailDbContext(DbContextOptions<MerchDetailDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MerchDetail>().ToTable("merchdetail").HasKey(k => k.MerchID);
        }
    }
}
