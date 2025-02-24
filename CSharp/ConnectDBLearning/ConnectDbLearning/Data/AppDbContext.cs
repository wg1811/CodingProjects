using ConnectDbLearning.Modules;
using Microsoft.EntityFrameworkCore;

namespace ConnectDbLearning.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        DbSet<Hotel> staff { get; set; }
    }
}
