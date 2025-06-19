using System.Text.RegularExpressions;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApi.Src.Modules.UserModule.Domain.VO;

public sealed class Password
{
    public string Value { get; }

    public Password(string value)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(value))
            errors.Add("A senha não pode ser vazia.");
        if (value.Length < 8)
            errors.Add("A senha deve ter no mínimo 8 caracteres.");
        if (!Regex.IsMatch(value, @"[A-Z]"))
            errors.Add("A senha deve conter pelo menos uma letra maiúscula.");
        if (!Regex.IsMatch(value, @"[a-z]"))
            errors.Add("A senha deve conter pelo menos uma letra minúscula.");
        if (!Regex.IsMatch(value, @"[0-9]"))
            errors.Add("A senha deve conter pelo menos um número.");
        if (!Regex.IsMatch(value, @"[\@\!\?\*\.\#\$\%\&]"))
            errors.Add("A senha deve conter pelo menos um caractere especial (@ ! ? * . # $ % &).");
        if (errors.Any())
            throw new ValidationException(errors);
        Value = value;
    }
    public override string ToString() => "********";
}
