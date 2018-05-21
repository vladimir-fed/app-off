using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace WebUI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private bool _disposed = false;
        private ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var response = new 
            {
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = 500,
                DeclaredType = response.GetType()
            };

            _logger.LogError("GlobalExceptionFilter", context.Exception);
        }
    }
}
