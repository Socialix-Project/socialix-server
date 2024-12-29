using Socialix.Common.API;

namespace Socialix.Controllers.Auth
{
    public class LoginRequest : ApiBaseRequest
    {
        public string UserName { get; set; }    
        public string Password { get; set; }   
    }
}
