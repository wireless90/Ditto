using System.Collections.Generic;

namespace Ditto.Common.Interfaces
{
    public interface ITransformBuilder
    {
        ITransformBuilder AddNewTransform(string transformName);

        ITransformBuilder AddClaim(string claimType, string value);

        List<Transform> Build();
    }
}
