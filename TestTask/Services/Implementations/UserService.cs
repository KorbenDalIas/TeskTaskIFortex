using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private const int orderYear = 2010;
        private const int orderYearSecondTask = 2003;
        public UserService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Where(x => x.Orders.Any(x => x.Status == Enums.OrderStatus.Paid && x.CreatedAt.Year == orderYear)).ToListAsync();
        }

        public async Task<User> GetUser()
        {
            var user = await Task.Run(() =>
                _context.Orders.Where(x => x.Status == OrderStatus.Delivered && x.CreatedAt.Year == orderYearSecondTask)
                .GroupBy(o => o.UserId).Select(o => new { UserId = o.Key, OrderCount = o.Count() }).OrderByDescending(o => o.OrderCount).FirstOrDefault());

            return await _context.Users.Where(x => x.Id == user.UserId).FirstOrDefaultAsync();
        }
    }
}
