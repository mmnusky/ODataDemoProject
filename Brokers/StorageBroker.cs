using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ODataDemoProject.Brokers
{
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStrings = configuration.
                GetConnectionString("PostgresConnection");

            optionsBuilder.UseNpgsql(connectionStrings);
        }
    }
}
