using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Ditto
{
    public class DittoOptions
    {
        public string AuthenticationType { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;

        public PathString Path { get; set; } = "/ditto";

        public List<Transform> Transforms { get; set; } = new List<Transform>();
    }
}
