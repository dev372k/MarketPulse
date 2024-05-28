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
    public interface ICustomerRepo
    {
        GetCustomerDTOs Get(int userId, int pageNo, int pageSize, string search);
        List<SelectListItem> Get(int userId);

        Task Add(int userId, AddCustomerDTO dto);

        void Update(int userId, UpdateCustomerDTO dto);

        void Delete(int id);
    }
}
