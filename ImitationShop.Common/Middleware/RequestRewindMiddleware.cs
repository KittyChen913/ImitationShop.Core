namespace ImitationShop.Common.Middleware;

public sealed class RequestRewindMiddleware
{
    private readonly RequestDelegate _next;

    public RequestRewindMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try { context.Request.EnableBuffering(); } catch { }
        await _next(context);
    }
}