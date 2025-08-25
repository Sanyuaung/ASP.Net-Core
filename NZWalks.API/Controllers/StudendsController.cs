using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudendsController : ControllerBase
    {
        private readonly ILogger<StudendsController> logger;
        public StudendsController(ILogger<StudendsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            logger.LogInformation("GetAllStudents called");
            string[] studentNames = new string[]
            {
                "John Doe",
                "Jane Smith",
                "Alice Johnson",
                "Bob Brown",
                "Charlie Davis",
                "Diana Prince",
                "Ethan Hunt"
            };
            return Ok(studentNames);
        }
    }
}
