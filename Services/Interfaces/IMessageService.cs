using app_test_api.Models;
using app_test_api.Models.DTOs;

namespace app_test_api.Services.Interfaces;

public interface IMessageService
{
    Task<List<MessageDto>> GetAllAsync();
    Task<Message?> GetByIdAsync(int id);
    Task<Message> CreateAsync(Message message);
    Task DeleteAsync(int id);
}
