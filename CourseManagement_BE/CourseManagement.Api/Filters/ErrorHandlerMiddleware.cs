namespace CourseManagement.Api.Filters
{
    using CourseManagement.Utilities.Errors;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

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
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var result = new CustomErrorObject(101, "some random error");

                await response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}
