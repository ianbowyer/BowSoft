using BowSoft.TestExtensions.TestingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BowSoft.TestExtensions.TestingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesterController : ControllerBase
    {
        [HttpGet("ReturnsJsonObject")]
        public IActionResult ReturnsJsonObject()
        {
            return Ok(new Customer { Id = 42, Name = "Ian Bowyer", City = "Leeds" });
        }

        [HttpGet("ReturnsHttpStatus/{httpStatusCode}")]
        public IActionResult ReturnsHttpStatus(HttpStatusCode httpStatusCode)
        {
            return new StatusCodeResult((int)httpStatusCode);
        }

        [HttpGet("ReturnsString/{stringToReturn}")]
        public IActionResult ReturnsString(string stringToReturn)
        {
            return Ok(stringToReturn);
        }

        [HttpGet("ReturnsNull")]
        public IActionResult ReturnsNull()
        {
            return null;
        }
    }
}