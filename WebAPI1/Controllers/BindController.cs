using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI1.Models;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindController : ControllerBase
    {
        #region Get
        //[HttpGet("{id:int}")]
        //[HttpGet]
        //public IActionResult testPrimite(int id,string? name)
        //{
        //    return Ok(id.ToString());
        //} 
        #endregion

        [HttpPost("{id:int}")]
        public IActionResult TestObj(Department dept,int id)
        {
            return Ok();
        }

        [HttpGet]//with no body
        //FromRoute api/bind/7637463/384728
        //FromQuery api/bind?Long=7637463&lat=384728
        public IActionResult GetLocation([FromQuery]Location loc)//Body
        {
            
            return Ok();
        }

    }
}
