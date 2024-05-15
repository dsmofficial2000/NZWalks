using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;
using System.Runtime.Versioning;

namespace NZWalks.Data
{
    public class NzWalksDBContext : DbContext
    {
        public NzWalksDBContext(DbContextOptions dbContextOptions) : base (dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> difficulties{ get; set; }
        public DbSet<Region> regions { get; set; }  
        public DbSet<Walk> walks { get; set; }
    }
}
