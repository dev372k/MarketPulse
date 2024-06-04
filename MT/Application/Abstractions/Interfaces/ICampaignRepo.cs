using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Application.Abstractions.Interfaces
{
    public interface ICampaignRepo
    {
        GetCampaignDTOs Get(int userId, int pageNo, int pageSize, string search);
        List<SelectListItem> Get(int userId);

        Task Add(int userId, AddCampaignDTO dto);

        void Update(int userId, UpdateCampaignDTO dto);

        void Delete(int id);
        Task Run(int id);
    }
}
