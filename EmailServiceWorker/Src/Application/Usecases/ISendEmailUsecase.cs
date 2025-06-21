namespace EmailServiceWorker.Src.Application.Usecases;

public interface ISendEmailUsecase
{
    Task ExecuteAsync(string email, string subject, string body);
}
