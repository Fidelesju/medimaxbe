using Newtonsoft.Json;
using System.Net;

namespace MediMax.Business.Exceptions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware ( RequestDelegate next )
        {
            _next = next;
        }

        public async Task Invoke ( HttpContext context )
        {
            try
            {
                await _next(context);
            }
            catch (CustomValidationException ex)
            {
                await HandleCustomValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGeneralExceptionAsync(context, ex);
            }
        }

        private static Task HandleCustomValidationExceptionAsync ( HttpContext context, CustomValidationException exception )
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400 Bad Request

            var errorContent = new Dictionary<string, string>();

            if (exception.IsStringError)
            {
                errorContent.Add("message", exception.Message);
            }
            else
            {
                foreach (var item in exception.Errors)
                {
                    errorContent.Add(item.Key, item.Value);
                }
            }

            var response = new
            {
                success = false,
                errors = errorContent,
                errorsList = exception.ErrorsList,
                failures = exception.Failures
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }


        private static Task HandleGeneralExceptionAsync ( HttpContext context, Exception exception )
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500 Internal Server Error

            var response = new
            {
                success = false,
                message = "An error occurred while processing your request."
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
