using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Models
{
    public class FinalProjectDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public FinalProjectDBContext(DbContextOptions<FinalProjectDBContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("Reservationservice");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Reservation> Reservation { get; set; } = null!;
        public DbSet<Restaurant> Restaurant { get; set; } = null!;


    }
}
