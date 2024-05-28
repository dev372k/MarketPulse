using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AddCampaignDTO
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public List<string> Groups { get; set; }
    }

    public class UpdateCampaignDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<string> Groups { get; set; }
    }

    public class GetCampaignDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string CreatedOn { get; set; }
        public List<GroupDTO> Groups { get; set; }
    }

    public class GetCampaignDTOs
    {
        public List<GetCampaignDTO> Item { get; set; }
        public int TotalCount { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
