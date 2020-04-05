using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GTSLogGeneratorApi.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        
        public async Task InvokeAsync(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException validationException)
            {
                var errors = validationException.Errors.Select(x => x.ErrorMessage).ToArray();
                await GetValidationProblemDetailsResponse(context, errors);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Unhandled exception. Message: {ex}");
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync(ex.ToString());
            }
        }
        
        private static async Task GetValidationProblemDetailsResponse(HttpContext context, IEnumerable<string> errors)
        {
            context.Response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(string.Join(Environment.NewLine, errors));
            await context.Response.WriteAsync(result);
        }
    }
}