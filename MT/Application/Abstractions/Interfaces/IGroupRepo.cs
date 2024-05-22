using Application.DTOs;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Interfaces
{
    public interface IGroupRepo
    {
        GetTodoDTOs Get(int userId, int pageNo, int pageSize, string search);

        void Add(int userId, AddGroupDTO dto);

    }
}
