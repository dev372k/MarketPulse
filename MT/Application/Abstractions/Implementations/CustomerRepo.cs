using Application.Abstractions.Interfaces;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Application.Abstractions.Implementations
{
    public class CustomerRepo : ICustomerRepo
    {
        private ApplicationDBContext _context;

        public CustomerRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task Add(int userId, AddCustomerDTO dto)
        {
            try
            {
                var isExist = _context.Customers.Any(_ => _.Email == dto.Email && _.UserId == userId);
                if (isExist)
                    throw new InvalidOperationException("Email already exists");

                var customer = new Customer
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    CreatedOn = DateTime.UtcNow,
                    UserId = userId
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();

                List<CustomerGroup> customerGroups = new();

                foreach (var group in dto.Groups)
                {
                    customerGroups.Add(new CustomerGroup
                    {
                        GroupId = ConversionHelper.ConvertTo<int>(group),
                        CustomerId = customer.Id
                    });
                }

                _context.CustomerGroups.AddRange(customerGroups);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        public async Task AddBulk(int userId, List<AddCustomerDTO> dtos)
        {
            //await _context.Customers.AddRangeAsync();
        }

        public void Update(int userId, UpdateCustomerDTO dto)
        {
            var customer = _context.Customers.FirstOrDefault(_ => _.Id == dto.Id);

            if (customer != null)
            {
                customer.Name = dto.Name;
                customer.Email = dto.Email;
                customer.Phone = dto.Phone;
                customer.UpdatedOn = DateTime.Now;


                var groups = _context.CustomerGroups.Where(_ => _.CustomerId == customer.Id).ToList();
                _context.CustomerGroups.RemoveRange(groups);

                List<CustomerGroup> customerGroups = new();

                foreach (var group in dto.Groups)
                {
                    customerGroups.Add(new CustomerGroup
                    {
                        GroupId = ConversionHelper.ConvertTo<int>(group),
                        CustomerId = customer.Id
                    });
                }

                _context.CustomerGroups.AddRange(customerGroups);
                _context.SaveChanges();
            }
        }

        public GetCustomerDTOs Get(int userId, int pageNo, int pageSize, string search)
        {
            var query = _context.Customers.Include(_ => _.CustomerGroups).Where(_ => _.UserId == userId).AsQueryable();
            var customers = query
                .Where(_ => !string.IsNullOrEmpty(search) ? _.Name.ToLower().Contains(search.ToLower()) : true)
                .Select(_ => new GetCustomerDTO
                {
                    Id = _.Id,
                    Name = _.Name,
                    Email = _.Email,
                    Phone = _.Phone,
                    Groups = _context.Groups.Where(g => _.CustomerGroups.Select(_ => _.GroupId).Contains(g.Id)).Select(g => new GroupDTO
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList(),
                    CreatedOn = _.CreatedOn.ToString("dd MMMM, yyyy")
                }).OrderByDescending(_ => _.Id).Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

            return new GetCustomerDTOs
            {
                Item = customers,
                PageNo = pageNo,
                PageSize = pageSize,
                TotalCount = string.IsNullOrEmpty(search) ? query.Count() : customers.Count()
            };
        }

        public void Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(_ => _.Id == id);

            if (customer != null)
            {
                customer.IsDeleted = true;
                _context.SaveChanges();
            }
        }

        public List<SelectListItem> Get(int userId)
        {
            return _context.Customers
                .Where(_ => _.UserId == userId)
                .Select(_ => new SelectListItem
                {
                    Value = _.Id.ToString(),
                    Text = _.Name,
                }).ToList();
        }
    }
}