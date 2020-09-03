using System.Collections.Generic;
using System.Security.Claims;

namespace Ditto
{
    public class Transform
    {
        public string TransformName { get; set; }

        public List<Claim> Claims { get; set; }

        public Transform(string transformName)
        {
            TransformName = transformName;
            Claims = new List<Claim>();
        }
    }
}
