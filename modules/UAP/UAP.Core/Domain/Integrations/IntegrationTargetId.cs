using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.Integrations;

public class IntegrationTargetId : TypedIdValueBase
{
    public IntegrationTargetId()
    {

    }

    public IntegrationTargetId(string id) : base(id)
    {

    }

    public IntegrationTargetId(Guid id) : base(id)
    {

    }
}