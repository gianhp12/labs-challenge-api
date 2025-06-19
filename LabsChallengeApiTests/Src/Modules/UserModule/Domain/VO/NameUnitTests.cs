using LabsChallengeApi.Src.Modules.UserModule.Domain.VO;
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
        Assert.IsTrue(result.Errors!.Contains("O nome não pode ser vazio."));
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenNameHasNumbers()
    {
        // GIVEN
        var input = "Gian123";
        // WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Name(input));
        // THEN
        Assert.IsTrue(result.Errors!.Any(e => e.Contains("O nome deve conter apenas letras")));
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenNameHasSpecialCharacters()
    {
        // GIVEN
        var input = "Gian@Henrique!";
        // WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Name(input));
        // THEN
        Assert.IsTrue(result.Errors!.Any(e => e.Contains("O nome deve conter apenas letras")));
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
