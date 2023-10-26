

using CleanMultitenantArchitecture.Infraestructure.Services;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITenantService _tenanService;

    public TenantMiddleware(RequestDelegate next,
        ITenantService tenantService)
    {
        _next = next;
        _tenanService = tenantService;
    }

    public async Task Invoke(HttpContext context)
    {
        var slug = context.Request.Path.Value?.Split('/')[1];

        if (!string.IsNullOrEmpty(slug) && context.RequestServices.GetRequiredService<IConfiguration>().GetConnectionString(slug) is string connectionString)
        {
            long.TryParse(slug, out long id);
            await _tenanService.SetTenantConnectionString(id,connectionString);
        }

        await _next(context);
    }
}

