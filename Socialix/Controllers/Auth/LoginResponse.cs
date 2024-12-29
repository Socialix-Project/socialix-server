using Socialix.Common.API;

namespace Socialix.Controllers.Auth
{
    public class LoginResponse : ApiBaseResponse<Token>
    {
        public override Token? Response { get; set; }
    }

    public class Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
