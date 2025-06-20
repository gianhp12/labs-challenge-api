using System.Text.RegularExpressions;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApi.Src.Modules.AuthModule.Domain.VO;

public sealed class Name
{
    public string Value { get; }

    public Name(string value)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(value))
            errors.Add("O nome não pode ser vazio.");
        if (!IsValid(value))
            errors.Add("O nome deve conter apenas letras e acentuação, não são permitidos números ou caracteres especiais.");
        if (errors.Any())
            throw new ValidationException(errors);
        Value = value;
    }

    private static bool IsValid(string name)
    {
        var pattern = @"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$";
        return Regex.IsMatch(name, pattern);
    }

    public override string ToString() => Value;
}
