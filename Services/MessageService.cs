using app_test_api.Data;
using app_test_api.Models;
using app_test_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app_test_api.Services;

public class MessageService : IMessageService
{
    private readonly AppDbContext _context;

    public MessageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Message>> GetAllAsync()
    {
        return await _context.Messages
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<Message?> GetByIdAsync(int id)
    {
        return await _context.Messages.FindAsync(id);
    }

    public async Task<Message> CreateAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task DeleteAsync(int id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message != null)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}
