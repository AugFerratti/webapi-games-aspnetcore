using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiGames_Demo.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }


        //Executes after action

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("### Executing -> OnActionExecuted");
            _logger.LogInformation("##################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation("##################################");
        }

        //Executes before action
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("### Executing -> OnActionExecuting");
            _logger.LogInformation("##################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
            _logger.LogInformation("##################################");
        }
    }
}
