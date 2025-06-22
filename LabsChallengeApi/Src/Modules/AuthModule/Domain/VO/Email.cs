using System.Text.RegularExpressions;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApi.Src.Modules.AuthModule.Domain.VO;

public sealed class Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationException("O email não pode ser vazio.");
        if (!IsValid(value))
            throw new ValidationException("O email informado é inválido.");
        Value = value;
    }

    private static bool IsValid(string email)
    {
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }
}
