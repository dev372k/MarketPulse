using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class GroupController : Controller
    {
        [HttpGet("Groups")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            //var res = _todoRepo.Get(_stateHelper.User().Id, pageNo == 0 ? 1 : pageNo, pageSize, search);

            return Ok(new
            {
                Data = new List<GetGroupDTOs>{
             new GetGroupDTOs
             {
                 Name = "Test 1",
                 CreatedOn = DateTime.Now.ToString("dd MMMM, yyyy")
             },
                          new GetGroupDTOs
             {
                 Name = "Test2",
                 CreatedOn = DateTime.Now.ToString("dd MMMM, yyyy")
             }
            }
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddGroupDTO request)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateGroupDTO request)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int id)
        {
            return Ok();
        }
    }
}
