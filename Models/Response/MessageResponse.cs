namespace app_test_api.Models.Response
{
    public sealed class MessageResponse
    {
        public int Id { get; init; }
        public string MessageContent { get; init; } = "";
        public string Sender { get; init; } = "";
        public DateTime CreatedAt { get; init; }
        public UserResponse User { get; init; } = new();
    }
}
