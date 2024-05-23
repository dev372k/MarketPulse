using Application.DTOs;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Application.Abstractions.Interfaces
{
    public interface IGroupRepo
    {
        GetTodoDTOs Get(int userId, int pageNo, int pageSize, string search);
        List<SelectListItem> Get(int userId);

        void Add(int userId, AddGroupDTO dto);

        void Update(int userId, UpdateGroupDTO dto);

        void Delete(int id);
    }
}
