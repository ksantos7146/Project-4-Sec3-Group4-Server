using Project_IV.Entities;

namespace Project_IV.Data
{
    public static class DbSeeder
    {
        public static void SeedData(GitCommitDbContext context)
        {
            // Seed Gender data
            if (!context.Genders.Any())
            {
                context.Genders.AddRange(
                    new Gender { Name = "Male" },
                    new Gender { Name = "Female" },
                    new Gender { Name = "Other" }
                );
                context.SaveChanges();
            }

            // Seed State data
            if (!context.States.Any())
            {
                context.States.AddRange(
                    new State { Name = "Online" },
                    new State { Name = "Offline" },
                    new State { Name = "Paused" }
                );
                context.SaveChanges();
            }
        }
    }
} 