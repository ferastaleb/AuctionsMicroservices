using Auctions.Monolithic.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auctions.Monolithic.Data
{
    public class AuctionsDbContext : DbContext
    {
        public DbSet<Outbid> Outbids { get; set; }
        public AuctionsDbContext(DbContextOptions<AuctionsDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
