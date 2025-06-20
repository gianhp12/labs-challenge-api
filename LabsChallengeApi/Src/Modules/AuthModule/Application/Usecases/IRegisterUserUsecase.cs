using LabsChallengeApi.Src.Modules.AuthModule.Application.Dtos.Input;

namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;

public interface IRegisterUserUsecase
{
    Task ExecuteAsync(RegisterUserInputDto dto);
}
