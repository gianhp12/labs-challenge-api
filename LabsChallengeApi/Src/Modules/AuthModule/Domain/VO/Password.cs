using System.Text.RegularExpressions;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApi.Src.Modules.AuthModule.Domain.VO;

public sealed class Password
{
    public string Value { get; }

    public Password(string value)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationException("A senha não pode ser vazia.");
        if (value.Length < 8)
            throw new ValidationException("A senha deve ter no mínimo 8 caracteres.");
        if (!Regex.IsMatch(value, @"[A-Z]"))
            throw new ValidationException("A senha deve conter pelo menos uma letra maiúscula.");
        if (!Regex.IsMatch(value, @"[a-z]"))
            throw new ValidationException("A senha deve conter pelo menos uma letra minúscula.");
        if (!Regex.IsMatch(value, @"[0-9]"))
            throw new ValidationException("A senha deve conter pelo menos um número.");
        Value = value;
    }
    public override string ToString() => "********";
}
