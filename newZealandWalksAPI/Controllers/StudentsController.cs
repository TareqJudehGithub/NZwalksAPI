using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace newZealandWalksAPI.Controllers
{
    // https://localhose:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = { "John Smith", "Sarah Adams", "Ali Karam", "William Tailor", "Ema Stones" };

            return Ok(studentNames); // response 200
        }
    }
}
