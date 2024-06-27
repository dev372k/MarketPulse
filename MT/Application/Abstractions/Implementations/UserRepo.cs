using Application.DTOs;
using Persistence;
using Persistence.Entities;
using Shared.Helpers;
using System.Reflection.Metadata;

namespace Application.Implementations
{
    public class UserRepo : IUserRepo
    {
        private ApplicationDBContext _context;

        public UserRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public int Add(RegisterDTO dto)
        {
            var userExist = Get(dto.Email);
            if (userExist != null)
                throw new Exception("User already exist with this email");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                
                PasswordHash = SecurityHelper.GenerateHash(dto.Password),
                SubscriptionType = Shared.enSubscriptionType.Free,
                PlanExpiry = DateTime.Now.AddMonths(1)
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public GetUserDTO Get(string email)
        {
            GetUserDTO? user = _context.Users.Where(_ => _.Email.ToLower().Equals(email.ToLower())).Select(_ => new GetUserDTO
            {
                Id = _.Id,
                Email = _.Email,
                Name = _.Name,
                Password = _.PasswordHash,
                Phone = _.Phone,
                Address = _.Address,
                City = _.City,
                State = _.State,
                Country = _.Country,
                SubscriptionType = Shared.enSubscriptionType.Free,
                PlanExpiry = _.PlanExpiry,
                IsExpired = _.PlanExpiry.Subtract(DateTime.Now).TotalDays <= 0 ? true: false,
            }).FirstOrDefault();
            return user;
        }

        public void Update(UpdateUserDTO dto)
        {
            var user = _context.Users.FirstOrDefault(_ => _.Id == dto.Id);
            if (user != null)
            {
                user.Name = dto.Name;
                user.Address = dto.Address;
                user.Phone = dto.Phone;
                user.City = dto.City;
                user.State = dto.State;
                user.Country = dto.Country;

                _context.SaveChanges();
            }
        }

        public IQueryable<GetUserDTO> Get()
        {
            var users = _context.Users.Select(_ => new GetUserDTO
            {
                Id = _.Id,
                Email = _.Email,
                Name = _.Name,
                Password = _.PasswordHash,
                Phone = _.Phone,
                Address = _.Address,
                City = _.City,
                State = _.State,
                Country = _.Country,
                SubscriptionType = Shared.enSubscriptionType.Free,
                PlanExpiry = _.CreatedOn.AddMonths(1),
                IsExpired = _.CreatedOn.AddMonths(1).Subtract(DateTime.Now).TotalDays <= 0 ? true : false,
                IsDeleted = _.IsDeleted
            });

            return users;
        }

        public void UpdateStatus(int userId, bool status)
        {
            var user = _context.Users.FirstOrDefault(_ => _.Id == userId);
            if(user != null)
            {
                user.IsDeleted = status;
                _context.SaveChanges();
            }
        }

        public (int, int) Count()
        {
            var users = _context.Users;
            return (users.Where(_ => !_.IsDeleted).Count(), users.Where(_ => _.IsDeleted).Count());
        }
    }
}
