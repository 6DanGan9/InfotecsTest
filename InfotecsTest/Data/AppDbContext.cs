using InfotecsTest.Model;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTest.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ValuesReport> ValuesReports { get; set; } = null!;
        public DbSet<ValuesResult> Results { get; set; } = null!;
        public DbSet<Value> Values { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // МАССОВАЯ ЗАМЕНА ПОВЕДЕНИЯ УДАЛЕНИЯ
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    bool isNullable = foreignKey.Properties.All(p => p.IsNullable);

                    if (isNullable)
                    {
                        foreignKey.DeleteBehavior = DeleteBehavior.SetNull;
                    }
                }
            }
        }
    }
}
