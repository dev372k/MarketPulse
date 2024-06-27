using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public interface IUserRepo
    {
        GetUserDTO Get(string email);
        int Add(RegisterDTO dto);
        void Update(UpdateUserDTO dto);
        IQueryable<GetUserDTO> Get();
        void UpdateStatus(int userId, bool status);
    }
}
