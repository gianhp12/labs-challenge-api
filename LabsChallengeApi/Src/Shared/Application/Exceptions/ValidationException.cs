namespace LabsChallengeApi.Src.Shared.Application.Exceptions;

public class ValidationException : Exception
{
    public List<string>? Errors { get; init; }
    public ValidationException() : base() { }
    public ValidationException(string message) : base(message) { }
    public ValidationException(List<string> errors)
    {
        Errors = errors;
    }
}
