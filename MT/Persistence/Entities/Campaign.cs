using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class Campaign : Base
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Groups { get; set; } = string.Empty;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
