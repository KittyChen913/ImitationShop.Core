namespace ImitationShop.Common.Extensions;

public static class CorsPolicyExtension
{
    public static void SetCorsPolicy(this IServiceCollection service, IConfiguration configuration)
    {
        var enableAllOrigins = configuration.GetSection("EnableAllowedAllOrigins").Get<bool>();
        var corsAllowedOrigins = configuration.GetSection("AllowedOrigins").Get<IEnumerable<string>>();

        if (enableAllOrigins)
        {
            service.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
        else
        {
            service.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy
                            .WithOrigins(corsAllowedOrigins.ToArray())
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}
