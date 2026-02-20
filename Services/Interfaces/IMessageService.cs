using app_test_api.Models;
using app_test_api.Models.Response;

namespace app_test_api.Services.Interfaces;

public interface IMessageService
{
    Task<List<MessageResponse>> GetAllAsync();
    Task<MessageResponse?> GetByIdAsync(int id);
    Task<MessageResponse> CreateAsync(Message message);
    Task DeleteAsync(int id);
}
