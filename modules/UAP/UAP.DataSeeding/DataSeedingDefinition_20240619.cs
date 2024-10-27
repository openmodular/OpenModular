using Microsoft.Extensions.Configuration;
using OpenModular.Common.Utils.Helpers;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Core.Domain.Configs;
using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.Module.UAP.Core.Infrastructure;
using System.Text.Json;
using OpenModular.Common.Utils.Extensions;

namespace OpenModular.Module.UAP.DataSeeding;

public class DataSeedingDefinition_20240619 : UAPDataSeedingDefinition
{
    /// <summary>
    /// 创世主
    /// </summary>
    public AccountId CreatorId = new("e0a953c3-ee8c-452b-9e6a-7d57d033bb00");

    public OrganizationId OrganizationId = new("e94b5f20-1b53-4de0-b2a0-15a5ff16bb9c");

    public override void Define()
    {
        //CreateOrganization();
        CreateCreator();
        CreateConfig();
    }

    private void CreateOrganization()
    {
        var org = Organization.Create(OrganizationId, "OpenModular", "001", "OpenModular", CreatorId);

        AddInsert(org);
    }

    private void CreateCreator()
    {
        var user = Account.Create(CreatorId, "openmodular", "service@openmodular.io", "15155555555", AccountStatus.Enabled);

        var passwordHasher = new PasswordHasher();

        user.PasswordHash = passwordHasher.HashPassword(user, "openmodular");

        AddInsert(user);
    }

    private void CreateConfig()
    {
        var config = new UAPConfig
        {
            Authentication = new AuthenticationConfig
            {
                Jwt = new JwtConfig
                {
                    Key = new StringHelper().GenerateRandom(),
                    Audience = "OpenModular",
                    Issuer = "OpenModular",
                    Expires = 120,
                    RefreshTokenExpires = 7
                }
            }
        };

        AddInsert(Config.Create(UAPConstants.ModuleCode, config.ToJson()));
    }
}