using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using NumberShop.Models.Tables;

namespace NumberShop.Models.Contexts
{
    public class MerchOrderDbContext : DbContext
    {
        public DbSet<MerchOrder> Orders { get; set; }
        public MerchOrderDbContext(DbContextOptions<MerchOrderDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MerchOrder>().ToTable("merchorder").HasKey(k => new { k.OrderID, k.MerchID });
        }
    }
}
