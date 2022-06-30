using Microsoft.EntityFrameworkCore;
using WebApp_UserStatisticsInfo.EF.Models;

namespace WebApp_UserStatisticsInfo.EF.Context
{
    public class UserStaticContext : DbContext
    {
        public DbSet<User_Statistics> User_Statistics => Set<User_Statistics>();

        public UserStaticContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Statistics>(entity =>
            {
                entity.HasKey(e => e.id_query);
            });
        }
    }
}
