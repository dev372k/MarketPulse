using Application.Abstractions.Implementations;
using Application.Abstractions.Interfaces;
using Application.DTOs;
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
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            return Ok();
        }
    }
}
