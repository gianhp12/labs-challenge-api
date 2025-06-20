namespace LabsChallengeApi.Src.Shared.Application.Exceptions.Dtos;

public class ErrorResponseDto
{
    public string Error { get; set; }
    public string Message { get; set; }

    public ErrorResponseDto(string error, string message)
    {
        Error = error;
        Message = message;
    }
}
