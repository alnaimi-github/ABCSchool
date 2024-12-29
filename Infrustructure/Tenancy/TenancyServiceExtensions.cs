using Application.Features.Tenancy;
using Finbuckle.MultiTenant;
using Infrastructure.Identity.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Infrastructure.Tenancy;

internal static class TenancyServiceExtensions
{
    internal static IServiceCollection AddMultiTenancy(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")!;

       return services.AddDbContext<TenantDbContext>(options =>
                options.UseSqlServer(connectionString))
                       .AddMultiTenant<AbcSchoolTenantInfo>()
                       .WithHeaderStrategy(TenancyConstants.TenantIdName)
                       .WithClaimStrategy(ClaimConstants.Tenant)
                       .WithCustomQueryStringStrategy(TenancyConstants.TenantIdName)
                       .WithEFCoreStore<TenantDbContext, AbcSchoolTenantInfo>()
                       .Services
                       .AddScoped<ITenantService,TenantService>();

    }

    private static MultiTenantBuilder<AbcSchoolTenantInfo> WithCustomQueryStringStrategy(
        this MultiTenantBuilder<AbcSchoolTenantInfo> builder,string customQueryStringStrategy )
    {

        return builder
            .WithDelegateStrategy(context =>
            {
                if (context is not HttpContext httpContext)
                {
                    return Task.FromResult(string.Empty!)!;
                }

                httpContext.Request.Query.TryGetValue(
                    customQueryStringStrategy,
                    out var tenantIdPram);

                return Task.FromResult(tenantIdPram.ToString())!;
            });
    }
}