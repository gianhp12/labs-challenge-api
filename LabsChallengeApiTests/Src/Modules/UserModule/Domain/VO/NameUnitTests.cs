using LabsChallengeApi.Src.Modules.AuthModule.Domain.VO;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Domain.VO;

[TestClass]
public class NameUnitTests
{
    [TestMethod]
    public void ShouldCreateName_WhenNameIsValid()
    {
        // GIVEN
        var input = "Gian Henrique Pereira";
        // WHEN
        var result = new Name(input);
        // THEN
        Assert.AreEqual(input, result.Value);
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenNameIsEmpty()
    {
        // GIVEN
        var input = "";
        // WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Name(input));
        // THEN
        Assert.AreEqual(result.Message, "O nome não pode ser vazio.");
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenNameHasNumbers()
    {
        // GIVEN
        var input = "Gian123";
        // WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Name(input));
        // THEN
        Assert.AreEqual(result.Message, "O nome deve conter apenas letras e acentuação, não são permitidos números ou caracteres especiais.");
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenNameHasSpecialCharacters()
    {
        // GIVEN
        var input = "Gian@Henrique!";
        // WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Name(input));
        // THEN
        Assert.AreEqual(result.Message, "O nome deve conter apenas letras e acentuação, não são permitidos números ou caracteres especiais.");
    }

    [TestMethod]
    public void ShouldCreateName_WhenNameHasAccents()
    {
        // GIVEN
        var input = "José da Silva";
        // WHEN
        var result = new Name(input);
        // THEN
        Assert.AreEqual(input, result.Value);
    }
}
