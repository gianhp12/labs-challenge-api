using System.Text.RegularExpressions;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApi.Src.Modules.AuthModule.Domain.VO;

public sealed class Name
{
    public string Value { get; }

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationException("O nome não pode ser vazio.");
        if (!IsValid(value))
            throw new ValidationException("O nome deve conter apenas letras e acentuação, não são permitidos números ou caracteres especiais.");
        Value = value;
    }

    private static bool IsValid(string name)
    {
        var pattern = @"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$";
        return Regex.IsMatch(name, pattern);
    }

    public override string ToString() => Value;
}
