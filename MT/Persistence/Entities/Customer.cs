using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class Customer
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string GroupTags { get; set; } = string.Empty;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
