namespace ImitationShop.Common.Extensions;

public static class ModelValidateExtensions
{
    public static void SetModelValidation(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = (context) =>
            {
                var loggerFactory = context.HttpContext.RequestServices
                    .GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("ModelValidateExtensions");

                var actionDescriptor =
                    (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor;

                var requestBody = context.HttpContext.Request.Body;
                if (requestBody.CanSeek) requestBody.Position = 0;
                var sr = new StreamReader(requestBody);
                string jsonBody = Regex.Replace(sr.ReadToEnd().Trim(), @"\s", "");

                logger.LogInformation(
                    $"Controller: {actionDescriptor.ControllerName}, Action: {actionDescriptor.ActionName}() - Request Body: {jsonBody}");

                string modelValidationResult = string.Join(" ", context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                throw new ValidationException(modelValidationResult);
            };
        });
    }
}