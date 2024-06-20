using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.DataSeeding;

internal class DataSeedingDefinition_20240619 : UAPDataSeedingDefinition
{
    public UserId CreatedBy = new UserId(Guid.Parse("e0a953c3-ee8c-452b-9e6a-7d57d033bb00"));

    public override void Define()
    {
        var user = User.Create("openmodular", "", "service@openmodular.io", "15155555555", CreatedBy);
        user.Id = new UserId(Guid.Parse(""));

        AddInsert(user);
    }
}