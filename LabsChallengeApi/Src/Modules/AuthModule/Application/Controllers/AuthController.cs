using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Application.Exceptions;
using LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
using LabsChallengeApi.Src.Shared.Application.Exceptions.Dtos;
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

        public AuthController(IAuthenticateUserUsecase authenticateUserUsecase, IRegisterUserUsecase registerUserUsecase)
        {
            _authenticateUserUsecase = authenticateUserUsecase;
            _registerUserUsecase = registerUserUsecase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Auth([FromBody] AuthenticateInputDto dto)
        {
            try
            {
                var result = await _authenticateUserUsecase.ExecuteAsync(dto);
                return Ok(result);
            }
            catch (EmailNotConfirmedException ex)
            {
                return BadRequest(new ErrorResponseDto("EMAIL_NOT_CONFIRMED", ex.Message));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ErrorResponseDto("VALIDATION_EXCEPTION", ex.Message));
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
                return BadRequest(new ErrorResponseDto("VALIDATION_EXCEPTION", ex.Message));
            }
        }
    }
}
