using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateUserUsecase _authenticateUserUsecase;
        private readonly IRegisterUserUsecase _registerUserUsecase;
        private readonly IValidateEmailTokenUsecase _validateEmailTokenUseCase;
        private readonly IResendEmailTokenUsecase _resendEmailTokenUsecase;

        public AuthController(
            IAuthenticateUserUsecase authenticateUserUsecase,
            IRegisterUserUsecase registerUserUsecase,
            IValidateEmailTokenUsecase validateEmailTokenUsecase,
            IResendEmailTokenUsecase resendEmailTokenUsecase
            )
        {
            _authenticateUserUsecase = authenticateUserUsecase;
            _registerUserUsecase = registerUserUsecase;
            _validateEmailTokenUseCase = validateEmailTokenUsecase;
            _resendEmailTokenUsecase = resendEmailTokenUsecase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Auth([FromBody] AuthenticateInputDto dto)
        {
            var result = await _authenticateUserUsecase.ExecuteAsync(dto);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserInputDto dto)
        {
            await _registerUserUsecase.ExecuteAsync(dto);
            return NoContent();
        }

        [HttpPost("validate-email-token")]
        public async Task<IActionResult> ValidateEmailToken([FromBody] ValidateEmailTokenInputDto dto)
        {
            await _validateEmailTokenUseCase.ExecuteAsync(dto);
            return Ok();
        }

        [HttpPost("resend-email-token")]
        public async Task<IActionResult> ResendEmailToken([FromBody] ResendEmailTokenInputDto dto)
        {
            await _resendEmailTokenUsecase.ExecuteAsync(dto.Email);
            return Accepted();
        }
    }
}
