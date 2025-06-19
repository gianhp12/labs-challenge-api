using LabsChallengeApi.Src.Modules.UserModule.Application.Dtos.Input;

namespace LabsChallengeApi.Src.Modules.UserModule.Application.Usecases;

public interface ICreateUserUsecase
{
    Task ExecuteAsync(CreateUserInputDto dto);
}
