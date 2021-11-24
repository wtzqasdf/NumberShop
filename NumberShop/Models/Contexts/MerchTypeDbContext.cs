using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using NumberShop.Models.Tables;

namespace NumberShop.Models.Contexts
{
    public class MerchTypeDbContext : DbContext
    {
        public DbSet<MerchType> MerchTypes { get; set; }

        public MerchTypeDbContext(DbContextOptions<MerchTypeDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MerchType>().ToTable("merchtype").HasKey(k => k.TypeID);
        }
    }
}
