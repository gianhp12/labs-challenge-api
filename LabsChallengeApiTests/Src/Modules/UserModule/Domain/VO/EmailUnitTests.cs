using LabsChallengeApi.Src.Modules.UserModule.Domain.VO;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Domain.VO;

[TestClass]
public class EmailUnitTests
{
    [TestMethod]
    public void ShouldCreateEmail_WhenEmailIsValid()
    {
        //GIVEN
        var input = "teste@gmail.com";
        //WHEN
        var result = new Email(input);
        //THEN
        Assert.AreEqual(input, result.Value);
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenEmailIsEmpty()
    {
        //GIVEN
        var input = "";
        //WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Email(input));
        //THEN
        Assert.IsTrue(result.Errors!.Contains("O email não pode ser vazio."));
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenEmailIsInvalid()
    {
        //GIVEN
        var input = "invalid_email";
        //WHEN
        var result = Assert.ThrowsException<ValidationException>(() => new Email(input));
        //THEN
        Assert.IsTrue(result.Errors!.Contains("O email informado é inválido."));
    }
}
