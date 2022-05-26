using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserDatabase.Entities;

namespace UserDatabase
{
    public class UserDbContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
            builder.AddConsole();
            //builder.AddFilter("System", LogLevel.Information);
        });

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder

            //    .UseLoggerFactory(loggerFactory)
            //    .EnableSensitiveDataLogging()
            //    .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignments;Integrated Security=SSPI");
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignments;Integrated Security=SSPI");
        }
    }
}
