using ApiWorkshop.Application.Middlewares;

namespace ApiWorkshop.Application.Domain.Extensions;

public static class RequestMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestMigrations(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MigrationMiddleware>();
    }
}
