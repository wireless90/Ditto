using Ditto.Common.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ditto
{
    public class TransformBuilder : ITransformBuilder
    {
        private Transform _currentTransform;

        private readonly List<Transform> transforms;

        public TransformBuilder()
        {
            _currentTransform = null;
            transforms = new List<Transform>();
        }

        public ITransformBuilder AddNewTransform(string transformName)
        {
            _currentTransform = new Transform(transformName.ToLower());
            transforms.Add(_currentTransform);

            return this;
        }

        public List<Transform> Build()
        {
            return transforms;
        }

        public ITransformBuilder AddClaim(string claimType, string value)
        {
            _currentTransform.Claims.Add(new Claim(claimType, value));

            return this;
        }
    }
}
