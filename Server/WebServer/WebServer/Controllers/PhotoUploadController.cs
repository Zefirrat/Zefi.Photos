using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotoUploadController : ControllerBase
    {
        private readonly ILogger<PhotoUploadController> _logger;

        public PhotoUploadController(ILogger<PhotoUploadController> logger)
        {
            _logger = logger;
        }

        [HttpGet("TestConnection")]
        public IActionResult TestConnection()
        {
            _logger.LogInformation("TestConnection working");
            return Ok();
        }
    }
}