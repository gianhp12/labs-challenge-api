using System.ComponentModel.DataAnnotations;
using LabsChallengeApi.Src.Modules.UserModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.UserModule.Application.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace LabsChallengeApi.Src.Modules.UserModule.Application.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserUsecase _createUserUsecase;

        public UserController(ICreateUserUsecase createUserUsecase)
        {
            _createUserUsecase = createUserUsecase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserInputDto dto)
        {
            try
            {
                await _createUserUsecase.ExecuteAsync(dto);
                return Created();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
