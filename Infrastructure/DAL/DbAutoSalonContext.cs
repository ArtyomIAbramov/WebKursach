using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebKursach.ApplicationCore.Models;

namespace WebKursach.Infrastructure.DAL
{
    public class DbAutoSalonContext : IdentityDbContext<User>
    {
        protected readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public DbAutoSalonContext(IConfiguration configuration, ILogger<DbAutoSalonContext> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            _logger.LogInformation("DbAutoSalonContext created at {DT}", DateTime.UtcNow.ToLongTimeString());
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
