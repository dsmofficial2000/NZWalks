using Microsoft.AspNetCore.Mvc;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentlist = new string[] { "Jayesh", "Parag", "Pramod", "Rahul", "Nikhil", "Sumit", "Durgesh"};
            return Ok(studentlist);
        }
    }
}
