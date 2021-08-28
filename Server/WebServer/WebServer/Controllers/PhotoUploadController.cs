using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebServer.Actions;

namespace WebServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotoUploadController : ControllerBase
    {
        private readonly ILogger<PhotoUploadController> _logger;
        private readonly IMediator _mediator;

        public PhotoUploadController(ILogger<PhotoUploadController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("TestConnection")]
        public IActionResult TestConnection()
        {
            _logger.LogInformation("Caught test connection");
            return Ok();
        }

        [HttpPost("EnsureFiles")]
        public async Task<ActionResult<EnsureFilesToUpload.ResultViewModel>> EnsureFilesToUpload(
            EnsureFilesToUpload.Request request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}