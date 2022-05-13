namespace ImitationShop.Common.Filter;

public class LogActionFilter : IAsyncActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionDescriptor =
            (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor;

        var requestBodyData = JsonSerializer.Serialize(context.ActionArguments.Values.FirstOrDefault());
        _logger.LogInformation(
            $"Controller: {actionDescriptor.ControllerName}, Action: {actionDescriptor.ActionName}() - Request Body: {requestBodyData}");

        var actionExecutedContext = await next();

        if (actionExecutedContext.Result != null)
        {
            var responseBodyData = JsonSerializer.Serialize((actionExecutedContext.Result as ObjectResult)?.Value);

            _logger.LogInformation(
                $"Controller: {actionDescriptor.ControllerName}, Action: {actionDescriptor.ActionName}() - Response Body: {responseBodyData}");
        }
    }
}