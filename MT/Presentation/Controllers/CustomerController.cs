using Application.Abstractions.Implementations;
using Application.Abstractions.Interfaces;
using Application.DTOs;
using Application.Implementations;
using AspNetCoreHero.ToastNotification.Abstractions;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Shared.Helpers;
namespace Presentation.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepo _customerRepo;
        private StateHelper _stateHelper;
        private readonly INotyfService _notyf;

        public CustomerController(ICustomerRepo customerRepo,
            INotyfService notyf,
             StateHelper stateHelper)
        {
            _customerRepo = customerRepo;
            _notyf = notyf;
            _stateHelper = stateHelper;
        }

        [HttpGet("Customers"), Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var res = _customerRepo.Get(_stateHelper.User().Id, pageNo == 0 ? 1 : pageNo, pageSize, search);

            return Ok(new ResponseModel { Data = res });
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerDTO request)
        {
            try
            {
                _customerRepo.Add(_stateHelper.User().Id, request);
                return Ok(new ResponseModel { Message = "Customer added successfully." });
            }
            catch (Exception)
            {
                return Ok(new ResponseModel {Status = false, Message = "Customer already exist." });
            }
        }

        [HttpPost]
        public IActionResult BulkPost(IFormFile file)
        {
            try
            {
                var customers = CsvHelperMethods.ReadCsvFile<AddCustomerDTO>(file);
                _customerRepo.AddBulk(_stateHelper.User().Id, customers);
                return Ok(new ResponseModel { Message = "Customers added successfully." });
            }
            catch (InvalidDataException ex)
            {
                return Ok(new ResponseModel { Status = false, Message = ex.Message });
            }
            catch (Exception)
            {
                return Ok(new ResponseModel { Status = false, Message = "An error occurred while adding customers." });
            }
        }


        [HttpPut]
        public IActionResult Put([FromBody] UpdateCustomerDTO request)
        {
            _customerRepo.Update(_stateHelper.User().Id,request);
            return Ok(new ResponseModel { Message = "Customer updated successfully." });
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            _customerRepo.Delete(id);
            return Ok(new ResponseModel { Message = "Customer deleted successfully." });
        }
    }
}
