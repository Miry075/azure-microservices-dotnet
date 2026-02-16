
using Wpm.Managemnt.Api.DataAccess;

namespace Wpm.Managemnt.Api.Extensions;

public static class ManagementDbContextExtension
{
    public static void EnsureDbIsCreated(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<ManagementDbContext>();
        context!.Database.EnsureCreated();
    }
}