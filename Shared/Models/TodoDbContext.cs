using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlazorApp1.Shared.Models
{
    public class TodoDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private string connectionString = "Server=Burazzo\\SQLEXPRESS;Database=TodoDb;Trusted_Connection=True; TrustServerCertificate=True";
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Category> Categories { get; set; }

        public TodoDbContext(DbContextOptions<TodoDbContext> options, IConfiguration configuration)
       : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .Property(x => x.Title)
                .IsRequired();
            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .IsRequired();
            modelBuilder.Entity<Person>()
               .Property(x => x.FirstName)
               .IsRequired();
            modelBuilder.Entity<Person>()
               .Property(x => x.LastName)
               .IsRequired();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
