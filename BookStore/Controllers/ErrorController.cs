using BookStore.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Exceptions;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger) 
        {
            _logger = logger;
        }

        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exceptionType = exceptionHandlerPathFeature.Error.GetType();

            ExceptionDetails exceptionDetails = new ExceptionDetails();

            if (!string.Equals(exceptionType.BaseType.Name, "ApplicationException"))
            {
                exceptionDetails.Message = "Server error";
                exceptionDetails.StatusCode = 500;
                exceptionDetails.IsApplicationError = false;
            }
            else
            {
                exceptionDetails.Message = exceptionHandlerPathFeature.Error.Message;
                exceptionDetails.StatusCode = 400;
                exceptionDetails.IsApplicationError = true;
                exceptionDetails.ExceptionType = exceptionType.Name;

                if (string.Equals(exceptionType.Name, "EntityValidationException"))
                {
                    exceptionDetails.Errors = ((EntityValidationException)exceptionHandlerPathFeature.Error).Errors;
                }

                if (string.Equals(exceptionType.Name, "ResourceNotFoundException"))
                {
                    exceptionDetails.StatusCode = 404;
                }
            }

            // Log
            string stacktrace = exceptionHandlerPathFeature.Error.StackTrace
                .Split(Environment.NewLine)
                .FirstOrDefault();

            _logger.LogError("{exception} occured at path: {path} \n" + stacktrace,
                exceptionType.Name, exceptionHandlerPathFeature.Path);

            HttpContext.Response.StatusCode = exceptionDetails.StatusCode;

            if (exceptionHandlerPathFeature.Path.Contains("admin"))
            {
                return View("~/Areas/Admin/Views/Error/Index.cshtml", exceptionDetails);
            }

            return View("~/Views/Error/Index.cshtml", exceptionDetails);
        }

        [Route("Error/{statusCode}")]
        public IActionResult NotFound(int statusCode)
        {
            ExceptionDetails exceptionDetails = new ExceptionDetails();
            exceptionDetails.Message = "404 page not found";
            exceptionDetails.StatusCode = statusCode;
            exceptionDetails.IsApplicationError = true;

            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            if (statusCodeResult.OriginalPath.Contains("admin"))
            {
                return View("~/Areas/Admin/Views/Error/Index.cshtml", exceptionDetails);
            }

            //Log
            _logger.LogWarning("404 page not found. Path: {path}", statusCodeResult.OriginalPath);

            return View("~/Views/Error/Index.cshtml", exceptionDetails);
        }
    }
}
