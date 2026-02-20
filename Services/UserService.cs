using app_test_api.Data;
using app_test_api.Models;
using app_test_api.Models.Request;
using app_test_api.Models.Response;
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

        public async Task<List<UserDetailResponse>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .OrderByDescending(u => u.CreatedAt)
                .Select(u => new UserDetailResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    CreatedAt = u.CreatedAt,
                    Messages = u.Messages.Select(m => new MessageResponse
                    {
                        Id = m.Id,
                        MessageContent = m.MessageContent,
                        Sender = m.Sender,
                        CreatedAt = m.CreatedAt,
                        User = new UserResponse
                        {
                            Id = u.Id,
                            Name = u.Name
                        }
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<UserDetailResponse?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == id)
                .Select(u => new UserDetailResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    CreatedAt = u.CreatedAt,
                    Messages = u.Messages.Select(m => new MessageResponse
                    {
                        Id = m.Id,
                        MessageContent = m.MessageContent,
                        Sender = m.Sender,
                        CreatedAt = m.CreatedAt,
                        User = new UserResponse
                        {
                            Id = u.Id,
                            Name = u.Name
                        }
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UserResponse> CreateAsync(CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}