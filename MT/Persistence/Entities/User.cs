namespace Persistence.Entities
{
    public class User  : Base
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<Group> Groups { get; set; }
    }
}
