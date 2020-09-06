using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;
using Eetfestijnkassasystem.Shared.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eetfestijnkassasystem.Api.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] // hide controller for swashbuckle swagger
    public class ErrorController : ControllerBase
    {
        private readonly ILogger _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("/error")]
        public IActionResult Error()
        {
            return _GetProblemDetailsResponse();
        }
       
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
                throw new InvalidOperationException("This shouldn't be invoked in non-development environments.");

            return _GetProblemDetailsResponse();
        }

        private ObjectResult _GetProblemDetailsResponse()
        {
            var exceptionContext = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (exceptionContext.Error is IEntityException ee)
            {
                _logger.LogError(exceptionContext.Error, ee.Type);
                return Problem(ee.StackTrace, null, (int)HttpStatusCode.BadRequest, ee.Message, ee.Type);
            }
            else
            { 
                _logger.LogError(exceptionContext.Error, exceptionContext.Error.Message);
                return Problem(detail: exceptionContext.Error.StackTrace, title: exceptionContext.Error.Message);
            }
        }
    }
}