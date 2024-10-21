using Microsoft.EntityFrameworkCore;
using SampleApi.Core.DataAccess.Models;

namespace SampleApi.Core.DataAccess.Context
{
    public class SampleDbContext(DbContextOptions<SampleDbContext> options) : DbContext(options)
    {
        public DbSet<DbProduct> Products { get; set; }
    }
}
