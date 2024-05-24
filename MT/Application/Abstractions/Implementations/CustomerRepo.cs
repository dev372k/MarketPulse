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
        public void Add(int userId, AddCustomerDTO dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                GroupTags = dto.Groups.toCSV(),
                CreatedOn = DateTime.Now,
                UserId = userId
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(int userId, UpdateCustomerDTO dto)
        {
            var customer = _context.Customers.FirstOrDefault(_ => _.Id == dto.Id);

            if (customer != null)
            {
                customer.Name = dto.Name;
                customer.Email = dto.Email;
                customer.GroupTags = dto.Groups.toCSV();
                customer.UpdatedOn = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public GetCustomerDTOs Get(int userId, int pageNo, int pageSize, string search)
        {
            var query = _context.Customers.Where(_ => _.UserId == userId).AsQueryable();
            var customers = query
                .Where(_ => !string.IsNullOrEmpty(search) ? _.Name.ToLower().Contains(search.ToLower()) : true)
                .Select(_ => new GetCustomerDTO
                {
                    Id = _.Id,
                    Name = _.Name,
                    Email = _.Email,
                    Groups = null,
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
            var group = _context.Groups.FirstOrDefault(_ => _.Id == id);

            if (group != null)
            {
                group.IsDeleted = true; 
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