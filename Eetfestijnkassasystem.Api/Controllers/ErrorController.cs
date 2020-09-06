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

namespace Eetfestijnkassasystem.Api.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] // hide controller for swashbuckle swagger
    public class ErrorController : ControllerBase
    {
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

            if (exceptionContext.Error is IEntityException ese)
                return Problem(ese.StackTrace, null, (int)HttpStatusCode.BadRequest, ese.Message, ese.Type);
            else
                return Problem(detail: exceptionContext.Error.StackTrace, title: exceptionContext.Error.Message);
        }
    }
}