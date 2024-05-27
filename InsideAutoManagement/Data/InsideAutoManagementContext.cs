using InsideAutoManagement.Data.Seed;
using InsideAutoManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace InsideAutoManagement.Data
{
    public class InsideAutoManagementContext : DbContext
    {
        IConfiguration _configuration { get; set; }

        public InsideAutoManagementContext (DbContextOptions<InsideAutoManagementContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InsideAutoManagementContext"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seeding.Initialize(this);
        }

        public DbSet<Configuration> Configurations { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarDealer> CarDealers { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<FolderCategory> FolderCategories { get; set; }

        public DbSet<OpeningHoursShift> OpeningHoursShifts { get; set; }
    }
}
