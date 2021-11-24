using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using NumberShop.Models.Tables;

namespace NumberShop.Models.Contexts
{
    public class CartDbContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public CartDbContext(DbContextOptions<CartDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().ToTable("cart").HasKey(k => new { k.Account, k.MerchID });
        }
    }
}
