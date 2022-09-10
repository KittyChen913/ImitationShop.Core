namespace ImitationShop.Common.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;
        var controllerName = context.GetRouteValue("controller")!.ToString();
        var actionName = context.GetRouteValue("action")!.ToString();
        var exceptionSource = exception.TargetSite!.DeclaringType!.FullName ?? MethodBase.GetCurrentMethod()?.Name;
        var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(exceptionSource!);

        BaseResponseModel<object> errorResponse = new();
        try
        {
            errorResponse.RequestId = JsonDocument.Parse(ReadRequestBody(context)).RootElement.GetProperty("RequestId").GetString();
        }
        catch (Exception)
        {
            // ignored
        }

        switch (exception)
        {
            case ValidationException ex:
                errorResponse.ErrorCode = ErrorCodeEnum.ParameterIsIncorrect.ToDescription();
                errorResponse.ErrorMessage = ex.Message;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case Exception ex:
                errorResponse.ErrorCode = ErrorCodeEnum.OtherSystemError.ToDescription();
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                errorResponse.ErrorCode = ErrorCodeEnum.OtherSystemError.ToDescription();
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var result = JsonSerializer.Serialize(errorResponse);
        logger.LogError(exception.ToString());

        logger.LogInformation(
            $"Controller: {controllerName}, Action: {actionName}() - Response Body: {result}");
        await context.Response.WriteAsync(result);
    }

    private string ReadRequestBody(HttpContext context)
    {
        var requestBody = context.Request.Body;
        if (requestBody.CanSeek) requestBody.Position = 0;
        var sr = new StreamReader(requestBody);
        string jsonBody = Regex.Replace(sr.ReadToEnd().Trim(), @"\s", "");
        return jsonBody;
    }
}