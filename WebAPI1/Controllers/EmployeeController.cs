using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI1.DTO;
using WebAPI1.Models;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ITIContext context;

        public EmployeeController(ITIContext Context)
        {
            context = Context;
        }

        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Employee> EmpList= context.Employee.ToList();
            //return Ok(EmpList);

            GeneralResponse response = new GeneralResponse();
            response.IsPass = true;
            response.Data = EmpList;

            return response;
        }


        [HttpGet("{id:int}")]
        public IActionResult GetDEails(int id)
        {
            //Get Data Source
            Employee? empModel= 
                context.Employee.Include(e=>e.Department)
                .FirstOrDefault(e => e.Id == id);
            //declare DTO
            EmpWithDeptDTO EmpDTO = new EmpWithDeptDTO();//ViewModel
            //Mapping
            EmpDTO.EmpName = empModel.Name;
            EmpDTO.EmpId = empModel.Id;
            EmpDTO.DeptName = empModel.Department.Name;//throw exception
            //send DTO
            return Ok(EmpDTO);
        }
    }
}
