using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tatweer.Application.Models;

namespace Tatweer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {


        public BaseController()
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-AU");
        }

        [Microsoft.AspNetCore.Mvc.NonAction]
        public virtual Result Problem(
           string? detail = null,
           string? instance = null,
           int? statusCode = null,
           string? title = null,
           string? type = null)
        {
            ProblemDetails problemDetails;
            if (ProblemDetailsFactory == null)
            {
                // ProblemDetailsFactory may be null in unit testing scenarios. Improvise to make this more testable.
                problemDetails = new ProblemDetails
                {
                    Detail = detail,
                    Instance = instance,
                    Status = statusCode ?? 500,
                    Title = title,
                    Type = type,
                };
            }
            else
            {
                problemDetails = ProblemDetailsFactory.CreateProblemDetails(
                    HttpContext,
                    statusCode: statusCode ?? 500,
                    title: title,
                    type: type,
                    detail: detail,
                    instance: instance);
            }

            return Result.Failure(new string[] { problemDetails.Detail });
        }

    }
}
