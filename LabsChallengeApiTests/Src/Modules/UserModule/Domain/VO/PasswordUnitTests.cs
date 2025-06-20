using LabsChallengeApi.Src.Modules.AuthModule.Domain.VO;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Domain.VO;

[TestClass]
public class PasswordUnitTests
{
    [TestMethod]
    public void ShouldCreatePassword_WhenPasswordIsValid()
    {
        //GIVEN
        var input = "ValidPass1@";
        //WHEN
        var result = new Password(input);
        //THEN
        Assert.AreEqual(input, result.Value);
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenPasswordIsEmpty()
    {
        //GIVEN
        var input = "";
        //WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Password(input));
        //THEN
        Assert.IsTrue(result.Errors!.Contains("A senha não pode ser vazia."));
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenPasswordHasLessThan8Characters()
    {
        //GIVEN
        var input = "V1@a";
        //WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Password(input));
        //THEN
        Assert.IsTrue(result.Errors!.Contains("A senha deve ter no mínimo 8 caracteres."));
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenPasswordDoesNotHaveUppercaseLetter()
    {
        //GIVEN
        var input = "validpass1@";
        //WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Password(input));
        //THEN
        Assert.IsTrue(result.Errors!.Contains("A senha deve conter pelo menos uma letra maiúscula."));
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenPasswordDoesNotHaveLowercaseLetter()
    {
        //GIVEN
        var input = "VALIDPASS1@";
        //WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Password(input));
        //THEN
        Assert.IsTrue(result.Errors!.Contains("A senha deve conter pelo menos uma letra minúscula."));
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenPasswordDoesNotHaveNumber()
    {
        //GIVEN
        var input = "ValidPass@";
        //WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Password(input));
        //THEN
        Assert.IsTrue(result.Errors!.Contains("A senha deve conter pelo menos um número."));
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenPasswordDoesNotHaveSpecialCharacter()
    {
        //GIVEN
        var input = "ValidPass1";
        //WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Password(input));
        //THEN
        Assert.IsTrue(result.Errors!.Contains("A senha deve conter pelo menos um caractere especial (@ ! ? * . # $ % &)."));
    }

    [TestMethod]
    public void ToString_ShouldReturnHiddenValue()
    {
        //GIVEN
        var input = "ValidPass1@";
        //WHEN
        var result = new Password(input);
        //THEN
        Assert.AreEqual("********", result.ToString());
    }
}

