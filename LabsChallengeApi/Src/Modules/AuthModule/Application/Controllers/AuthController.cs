using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
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
            try
            {
                var result = await _authenticateUserUsecase.ExecuteAsync(dto);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserInputDto dto)
        {
            try
            {
                await _registerUserUsecase.ExecuteAsync(dto);
                return Created();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("validate-email-token")]
        public async Task<IActionResult> ValidateEmailToken([FromBody] ValidateEmailTokenInputDto dto)
        {
            try
            {
                await _validateEmailTokenUseCase.ExecuteAsync(dto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("resend-email-token")]
        public async Task<IActionResult> ResendEmailToken([FromBody] string email)
        {
            try
            {
                await _resendEmailTokenUsecase.ExecuteAsync(email);
                return Accepted();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }
    }
}
