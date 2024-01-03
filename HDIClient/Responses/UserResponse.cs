using HDIClient.DTOs;

namespace HDIClient.Responses
{
    public class UserResponse
    {
        public TokenDTO? token { get; set; }
        public string? message { get; set; }
        public string? error { get; set; }
    }
}
