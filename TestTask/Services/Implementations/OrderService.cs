using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private const int minQuantity = 1;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Where(x => x.User.Status == Enums.UserStatus.Active).OrderBy(x => x.CreatedAt).ToListAsync();
        }

        public async Task<Order> GetOrder()
        {
            return await _context.Orders.Where(x => x.Quantity > minQuantity).OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        }
    }
}
