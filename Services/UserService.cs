using app_test_api.Data;
using app_test_api.Models;
using app_test_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app_test_api.Services
{
    public class UserService : IUsersService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
             _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }
    }
}