using Shared;

namespace Persistence.Entities
{
    public class User : Base
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;


        public enSubscriptionType SubscriptionType { get; set; } = enSubscriptionType.Free;
        public DateTime PlanExpiry { get; set; } = DateTime.Now.AddMonths(1);
        public ICollection<Group> Groups { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
