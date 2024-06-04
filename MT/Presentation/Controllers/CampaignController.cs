using Application.Abstractions.Interfaces;
using Application.DTOs;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class CampaignController : Controller
    {
        
        private StateHelper _stateHelper;
        private readonly ICampaignRepo _campaignRepo;
        private readonly INotyfService _notyf;

        public CampaignController(ICampaignRepo campaignRepo,
            INotyfService notyf,
             StateHelper stateHelper)
        {
            _campaignRepo = campaignRepo;
            _notyf = notyf;
            _stateHelper = stateHelper;
        }

        [HttpGet("Campaigns"), Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var res = _campaignRepo.Get(_stateHelper.User().Id, pageNo == 0 ? 1 : pageNo, pageSize, search);

            return Ok(new ResponseModel { Data = res });
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCampaignDTO request)
        {
            _campaignRepo.Add(_stateHelper.User().Id, request);
            return Ok(new ResponseModel { Message = "Campaign added successfully." });
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateCampaignDTO request)
        {
            _campaignRepo.Update(_stateHelper.User().Id, request);
            return Ok(new ResponseModel { Message = "Campaign updated successfully." });
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            _campaignRepo.Delete(id);
            return Ok(new ResponseModel { Message = "Campaign deleted successfully." });
        }
        
        [HttpGet]
        public async Task<IActionResult> Run([FromQuery] int id)
        {
            await _campaignRepo.Run(id);
            return Ok(new ResponseModel { Message = "Campaign ran successfully." });
        }
    }
}
