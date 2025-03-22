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
        public DbSet<Gender> Genders { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Bio).HasColumnName("bio").HasColumnType("text");
                entity.Property(e => e.GenderId).HasColumnName("gender_id");
                entity.Property(e => e.StateId).HasColumnName("state_id");
                entity.Property(e => e.Age).HasColumnName("age");

                entity.HasOne(e => e.Gender)
                    .WithMany()
                    .HasForeignKey(e => e.GenderId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.State)
                    .WithMany()
                    .HasForeignKey(e => e.StateId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("images");
                entity.Property(e => e.ImageId).HasColumnName("image_id");
                entity.Property(e => e.ImageData).HasColumnName("image_data").HasColumnType("text").IsRequired();
                entity.Property(e => e.UploadedAt).HasColumnName("uploaded_at");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Images)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("likes");
                entity.Property(e => e.LikeId).HasColumnName("like_id");
                entity.Property(e => e.LikerId).HasColumnName("liker_id");
                entity.Property(e => e.LikedId).HasColumnName("liked_id");
                entity.Property(e => e.LikedAt).HasColumnName("liked_at");

                entity.HasOne(e => e.Liker)
                    .WithMany(e => e.LikesGiven)
                    .HasForeignKey(e => e.LikerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Liked)
                    .WithMany(e => e.LikesReceived)
                    .HasForeignKey(e => e.LikedId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("matches");
                entity.Property(e => e.MatchId).HasColumnName("match_id");
                entity.Property(e => e.User1Id).HasColumnName("user1_id");
                entity.Property(e => e.User2Id).HasColumnName("user2_id");
                entity.Property(e => e.MatchedAt).HasColumnName("matched_at");

                entity.HasOne(e => e.User1)
                    .WithMany(e => e.MatchesAsUser1)
                    .HasForeignKey(e => e.User1Id)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.User2)
                    .WithMany(e => e.MatchesAsUser2)
                    .HasForeignKey(e => e.User2Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.ToTable("preferences");
                entity.Property(e => e.PreferenceId).HasColumnName("preference_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.MinAge).HasColumnName("min_age").HasDefaultValue(18);
                entity.Property(e => e.MaxAge).HasColumnName("max_age").HasDefaultValue(100);
                entity.Property(e => e.GenderId).HasColumnName("gender_id");

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Preferences)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Gender)
                    .WithMany()
                    .HasForeignKey(e => e.GenderId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(20).IsRequired();
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("state");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(20).IsRequired();
            });
        }
    }
}
