using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFinance.WebApi.Middlewares
{
    public class LogRequestResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogRequestResponseMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            _logger.Information("Request: {method} - {path} QueryString: {queryString} - Body: ", request.Method, request.Path, request.QueryString, request.Body);

            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            responseBody.Seek(0, SeekOrigin.Begin);
            var response = await new StreamReader(responseBody).ReadToEndAsync();

            _logger.Information("Response: {statusCode} {response}", context.Response.StatusCode, response);

            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }


    }
}
