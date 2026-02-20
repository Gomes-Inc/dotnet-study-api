namespace app_test_api.Models.DTOs
{
    public sealed class MessageDto
    {
        public int Id { get; init; }
        public string MessageContent { get; init; } = "";
        public string Sender { get; init; } = "";
        public DateTime CreatedAt { get; init; }
        public UserDto User { get; init; } = new();
    }
}
