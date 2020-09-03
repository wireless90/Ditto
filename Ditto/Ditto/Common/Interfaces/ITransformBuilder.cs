using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Ditto.Common.Interfaces
{
    public interface ITransformBuilder
    {
        ITransformBuilder AddNewTransform(string transformName);

        ITransformBuilder AddClaim(string claimType, string value);

        List<Transform> Build();
    }
}
