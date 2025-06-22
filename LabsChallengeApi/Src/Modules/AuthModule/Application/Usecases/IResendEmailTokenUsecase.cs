namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Usecases;

public interface IResendEmailTokenUsecase
{
    Task ExecuteAsync(string email);
}
