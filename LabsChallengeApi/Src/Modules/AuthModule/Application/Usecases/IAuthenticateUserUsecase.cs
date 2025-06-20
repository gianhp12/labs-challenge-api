using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;
using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Output;

namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;

public interface IAuthenticateUserUsecase
{
    Task<AuthenticatedUserOutputDto> ExecuteAsync(AuthenticateInputDto dto);
}
