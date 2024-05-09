using BookStore.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;
using System;

namespace BookStore.Controllers
{
    public class ErrorController : Controller
    {

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
            exceptionDetails.ExceptionType = "404";

            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                var originalPath = HttpContext.Items["originalPath"] as string;
                if (originalPath.Contains("admin"))
                {
                    return View("~/Areas/Admin/Views/Error/Index.cshtml", exceptionDetails);
                }
            }

            return View("~/Views/Error/Index.cshtml", exceptionDetails);
        }
    }
}
