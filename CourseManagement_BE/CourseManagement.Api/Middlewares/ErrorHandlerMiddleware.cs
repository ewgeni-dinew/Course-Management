namespace CourseManagement.Api.Middlewares
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using CourseManagement.Utilities.Errors;
    using CourseManagement.Utilities.Errors.Attributes;

    internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            // catch () { } COULD BE USED TO CATCH ALL OTHER TYPES OF EXCEPTIONS
            catch (CustomException ex)
            {
                Console.WriteLine("---ERROR---: " + ex.Message); //used for testing purposes only

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;

                await response.WriteAsync(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("---ERROR---: " + ex.Message); //used for testing purposes only

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;

                var result = new CustomErrorObject((int)ErrorMessages.GENERAL_ERROR, ErrorMessages.GENERAL_ERROR.GetMessage());

                await response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}
