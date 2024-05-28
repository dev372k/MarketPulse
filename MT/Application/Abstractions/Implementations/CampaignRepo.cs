using Application.Abstractions.Interfaces;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Shared.Helpers;
using System.Web.Mvc;

namespace Application.Abstractions.Implementations
{
    public class CampaignRepo : ICampaignRepo
    {
        private ApplicationDBContext _context;

        public CampaignRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task Add(int userId, AddCampaignDTO dto)
        {
            var campaign = new Campaign
            {
                Name = dto.Name,
                Content = dto.Content,
                CreatedOn = DateTime.Now,
                UserId = userId
            };
            _context.Campaigns.Add(campaign);
            _context.SaveChanges();

            List<CampaignGroup> campaignGroups = new();

            foreach (var group in dto.Groups)
            {
                campaignGroups.Add(new CampaignGroup
                {
                    GroupId = ConversionHelper.ConvertTo<int>(group),
                    CampaignId = campaign.Id
                });
            }

            _context.CampaignGroups.AddRange(campaignGroups);
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var campaign = _context.Campaigns.FirstOrDefault(_ => _.Id == id);

            if (campaign != null)
            {
                campaign.IsDeleted = true;
                _context.SaveChanges();
            }
        }

        public GetCampaignDTOs Get(int userId, int pageNo, int pageSize, string search)
        {
            var query = _context.Campaigns.Include(_ => _.CampaignGroup).Where(_ => _.UserId == userId).AsQueryable();
            var campaigns = query
                .Where(_ => !string.IsNullOrEmpty(search) ? _.Name.ToLower().Contains(search.ToLower()) : true)
                .Select(_ => new GetCampaignDTO
                {
                    Id = _.Id,
                    Name = _.Name,
                    Content = _.Content,
                    Groups = _context.Groups.Where(g => _.CampaignGroup.Select(_ => _.GroupId).Contains(g.Id)).Select(g=>new GroupDTO
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList(),
                    CreatedOn = _.CreatedOn.ToString("dd MMMM, yyyy")
                }).OrderByDescending(_ => _.Id).Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

            return new GetCampaignDTOs
            {
                Item = campaigns,
                PageNo = pageNo,
                PageSize = pageSize,
                TotalCount = string.IsNullOrEmpty(search) ? query.Count() : campaigns.Count()
            };
        }

        public List<SelectListItem> Get(int userId)
        {
            return _context.Campaigns
                .Where(_ => _.UserId == userId)
                .Select(_ => new SelectListItem
                {
                    Value = _.Id.ToString(),
                    Text = _.Name,
                }).ToList();
        }

        public void Update(int userId, UpdateCampaignDTO dto)
        {
            var campaign = _context.Campaigns.FirstOrDefault(_ => _.Id == dto.Id);

            if (campaign != null)
            {
                campaign.Name = dto.Name;
                campaign.Content = dto.Content;
                campaign.UpdatedOn = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
