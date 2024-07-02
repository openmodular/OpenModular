using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Core.Infrastructure;

namespace OpenModular.Module.UAP.DataSeeding;

internal class DataSeedingDefinition_20240619 : UAPDataSeedingDefinition
{
    /// <summary>
    /// 创世主
    /// </summary>
    public UserId CreatorId = new("e0a953c3-ee8c-452b-9e6a-7d57d033bb00");

    public override void Define()
    {
        CreateOrganization();
        CreateCreator();
    }

    private void CreateOrganization()
    {
        var org = Organization.Create("OpenModular", "001", "OpenModular", CreatorId);

        AddInsert(org);
    }

    private void CreateCreator()
    {
        var user = User.Create(CreatorId, "openmodular", "service@openmodular.io", "15155555555", CreatorId);

        user.SetPasswordHash(new PasswordHasher().HashPassword(user, "openmodular"));

        user.Activate();

        AddInsert(user);
    }
}