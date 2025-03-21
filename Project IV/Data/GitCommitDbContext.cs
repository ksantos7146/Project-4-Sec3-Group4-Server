using Microsoft.EntityFrameworkCore;
using Project_IV.Entities;

namespace Project_IV.Data
{
    public class GitCommitDbContext : DbContext
    {
        public GitCommitDbContext(DbContextOptions<GitCommitDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Preference> Preferences { get; set; }
    }
}
