using Application.Features.Tenancy;
using Application.Features.Tenancy.Commands;

namespace Infrastructure.Tenancy;

internal class TenantService : ITenantService
{
    public async Task<string> createTenantAsync(CreateTenantCommand tenantCommand)
    {
        throw new NotImplementedException();
    }
}