using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TicsaAPI.DAL.Model;
using TicsaAPI.DAL;

namespace TicsaAPI.DAL
{
    public class TicsaContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Gamme> Gammes { get; set; }
        public DbSet<GammeType> GammeTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderContent> OrderContents { get; set; }
        public DbSet<Client> Clients { get; set; }
        public TicsaContext()
        {

        }
        public TicsaContext(DbContextOptions<TicsaContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(@"Data Source=C:\blogging.db");
    }
}
