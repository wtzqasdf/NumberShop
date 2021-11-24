using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using NumberShop.Models.Tables;

namespace NumberShop.Models.Contexts
{
    public class SessionDbContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }

        public SessionDbContext(DbContextOptions<SessionDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().ToTable("session").HasKey(k => k.Token);
        }
    }
}
