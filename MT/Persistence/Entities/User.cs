using Shared;

namespace Persistence.Entities
{
    public class User  : Base
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public enSubscriptionType SubscriptionType { get; set; } = enSubscriptionType.Free;
        public ICollection<Group> Groups { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
