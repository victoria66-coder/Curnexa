namespace Curnexa.Models.ViewModels.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid BaseCurrencyId { get; set; }
    }
}
