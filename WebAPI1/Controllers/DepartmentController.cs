using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI1.Models;

namespace WebAPI1.Controllers
{
    /*int the sam project create MVC  & web api*/
    [Route("api/[controller]")]//api/Department
    [ApiController]//filter api controller "wait"
    public class DepartmentController : ControllerBase
    {
        private readonly ITIContext context;

        public DepartmentController(ITIContext context)
        {
            this.context = context;
        }
        //api/Department  Get
        [HttpGet]
        [Authorize]//check Token
        public IActionResult DisplayAll() {
            List<Department> DeptList = context.Department.ToList();
            //httpresponse (200 ,DeptList)
            return Ok(DeptList);
        }
       
        [HttpGet("{id:int}")]//api/Departmetn/1
        public ActionResult<GeneralResponse> GetByID(int id)
        {
            Department dept = context.Department.FirstOrDefault(d=>d.Id==id);
            GeneralResponse response = new GeneralResponse();
            if (dept == null)
            {
                response.IsPass = false;
                response.Data = "Invalid ID";
            }
            else
            {
                response.IsPass = true;
                response.Data = dept;
            }
            return response;
        }

        [HttpGet("{name:alpha}")]//api/DEpartment/ss
        public IActionResult GetByName(string name)
        {
            Department dept = 
                context.Department.FirstOrDefault(d => d.Name.Contains(name));
            return Ok(dept);
        }

        //api/Department  Post  Create new Resourse
        [HttpPost]
        public IActionResult Add(Department newDept) {
            if (ModelState.IsValid)
            {
                context.Add(newDept);
                context.SaveChanges();
               // return Created($"http://localhost:61461/api/department/{newDept.Id}",newDept);
                return CreatedAtAction("GetByID", new { id=newDept.Id}, newDept);
            }
            return BadRequest(ModelState);
        }

        ////api/Department  Put
        [HttpPut("{id:int}")]
        public IActionResult Edit(Department deptFromReq ,int id) {
            if (ModelState.IsValid)
            {
                Department oldDept = context.Department.FirstOrDefault(d => d.Id == id);
                oldDept.Name=deptFromReq.Name;
                oldDept.ManagerName=deptFromReq.ManagerName;
                context.SaveChanges();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        //api/Department  Delete
        [HttpDelete("{id:int}")]
        public IActionResult remove(int id) {
            Department oldDept = context.Department.FirstOrDefault(d => d.Id == id);
            context.Remove(oldDept);
            context.SaveChanges();
            return NoContent();

        }


        #region CRUD


        #endregion


    }
}
