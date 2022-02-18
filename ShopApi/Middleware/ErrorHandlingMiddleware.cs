using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShopApi.Exeptions;
using System;
using System.Threading.Tasks;

namespace ShopApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ForbidExeption forbidExeption)
            {
                context.Response.StatusCode = 403;

            }
            catch (BadRequestException badRequestExeption)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestExeption.Message);
            }
            catch (NotFoundExeption notFoundExeption)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundExeption.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
