using System.Text.RegularExpressions;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApi.Src.Modules.AuthModule.Domain.VO;

public sealed class Email
{
    public string Value { get; }

    public Email(string value)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(value))
            errors.Add("O email não pode ser vazio.");
        if (!IsValid(value))
            errors.Add("O email informado é inválido.");
        if (errors.Any())
        {
            throw new ValidationException(errors);
        }
        Value = value;
    }

    private static bool IsValid(string email)
    {
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }
}
