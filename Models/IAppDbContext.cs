using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MTCRUD.Models
{
    public interface IAppDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrdersDetails { get; set; }

        Task<int> SaveChanges();
    }
}
