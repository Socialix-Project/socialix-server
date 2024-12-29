using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Socialix.Common.API;
using Socialix.Common.Constants;
using Socialix.Common.Validators;
using Socialix.Repositories.Interfaces;

namespace Socialix.Controllers.Auth
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public LoginController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        private LoginResponse ErrorCheck(LoginRequest request)
        {
            var response = new LoginResponse() { Success = false };
            var detailErrors = new List<DetailError>();

            var inputFieldUserName = new InputField<string>() { FieldName = nameof(request.UserName), Value = request.UserName };
            InputCheck<string>.CheckRequired(inputFieldUserName, detailErrors);

            var inputFieldPassword = new InputField<string>() { FieldName = nameof(request.Password), Value = request.Password };
            InputCheck<string>.CheckRequired(inputFieldPassword, detailErrors);

            if (detailErrors.Any())
            {
                response.MessageId = Message.E00010;
                response.Message = Message.GetMessageById(Message.E00010);
                response.DetailErrorList = detailErrors;
                return response;
            }

            response.Success = true;
            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Exec([FromBody] LoginRequest request)
        {
            var response = ErrorCheck(request);
            if (!response.Success) return BadRequest(response);

            if (request.IsOnlyValidation) return Ok(response);

            var accessToken = await _authRepository.LoginAsync(request.UserName, request.Password);
            if (string.IsNullOrEmpty(accessToken)) return Unauthorized();

            response.Success = true;
            response.MessageId = Message.I00001;
            response.Message = Message.GetMessageById(Message.I00001);
            response.Response.AccessToken = accessToken;
            return Ok(response);
        }

    }
}
