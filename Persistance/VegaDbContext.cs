using Microsoft.EntityFrameworkCore;

namespace Vega.Persistance
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) :base(options)
        {
            
        }
    }
}