using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Data
{
    public class KariyerAppDBContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-3PER35R; Database=furkanmar; uid=root; pwd=123456;");
        }

        public DbSet<Advert> Adverts { get; set; }
    }
}
