using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MTCRUD.Models
{
    public class AppDBContext : DbContext, IAppDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrdersDetails { get; set; }

        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

    }
}
