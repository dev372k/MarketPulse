using Application.Abstractions.Implementations;
using Application.Abstractions.Interfaces;
using Application.DTOs;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class GroupController : Controller
    {
        private IGroupRepo _groupRepo;
        private StateHelper _stateHelper;

        public GroupController(IGroupRepo groupRepo,
            StateHelper stateHelper)
        {
            _groupRepo = groupRepo;
            _stateHelper = stateHelper;
        }

        [HttpGet("Groups"), Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var res = _groupRepo.Get(_stateHelper.User().Id, pageNo == 0 ? 1 : pageNo, pageSize, search);

            return Ok(new ResponseModel { Data = res });
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddGroupDTO request)
        {
            _groupRepo.Add(_stateHelper.User().Id, request);
            return Ok(new ResponseModel { Message = "Group added successfully." });
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateGroupDTO request)
        {
            _groupRepo.Update(_stateHelper.User().Id, request);
            return Ok(new ResponseModel { Message = "Group updated successfully." });
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            _groupRepo.Delete(id);
            return Ok(new ResponseModel { Message = "Group deleted successfully." });
        }
    }
}
