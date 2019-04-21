using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace siberianoilrush_server.Auth
{
    public static class AuthOptions
    {
        public const string ISSUER = "SORServer";
        public const string AUDIENCE = "http://localhost:50001/";
        public const int LIFETIME = 1440000;

        private const string KEY = "";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}
