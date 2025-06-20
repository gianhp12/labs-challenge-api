using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApi.Src.Modules.AuthModule.Application.Exceptions;

public class EmailNotConfirmedException : ValidationException
{
    public EmailNotConfirmedException(string message) : base(message) { }
}
