namespace EmailServiceWorker.Src.Application.Usecases;

public interface ISendConfirmationTokenEmailUsecase
{
    Task ExecuteAsync(string email, string token);
}
