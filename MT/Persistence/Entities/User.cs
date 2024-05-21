namespace Persistence.Entities
{
    public class User  : Base
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int MyProperty { get; set; }
    }
}
