using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities
{
    public class Group : Base
    {
        public string Name { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
