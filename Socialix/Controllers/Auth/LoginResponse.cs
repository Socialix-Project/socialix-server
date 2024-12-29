using Socialix.Common.API;

namespace Socialix.Controllers.Auth
{
    public class LoginResponse : ApiBaseResponse<Token>
    {
        public override Token? Response { get; set; }
    }
}
