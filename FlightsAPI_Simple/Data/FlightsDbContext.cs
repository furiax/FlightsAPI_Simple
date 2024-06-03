using Microsoft.EntityFrameworkCore;

namespace FlightsAPI_Simple.Data
{
    public class FlightsDbContext : DbContext
    {
        public FlightsDbContext(DbContextOptions options) : base(options)
        {
                
        }
    }
}
