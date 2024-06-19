using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence;

namespace OpenModular.Module.UAP.DataSeeding;

internal class DataSeedingDefinition_20240619 : UAPDataSeedingDefinition
{
    public override async Task Define(UAPDbContext dbContext)
    {
        var user = User.Create("", "", "", "", new UserId(Guid.Parse("")));
        user.Id = new UserId(Guid.Parse(""));

        await dbContext.Users.AddAsync(user);
    }
}