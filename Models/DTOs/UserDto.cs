namespace app_test_api.Models.DTOs
{
    public sealed class UserDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
        public DateTime CreatedAt { get; init; }
        public List<MessageDto> Messages { get; init; } = new();
    }
}
