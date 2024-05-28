using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{

    public class AddCustomerDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Groups { get; set; }
    }

    public class UpdateCustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Groups { get; set; }
    }

    public class GetCustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreatedOn { get; set; }
        public List<GroupDTO> Groups { get; set; }
    }

    public class GetCustomerDTOs
    {
        public List<GetCustomerDTO> Item { get; set; }
        public int TotalCount { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
