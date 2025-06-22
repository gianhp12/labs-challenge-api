using LabsChallengeApi.Src.Modules.AuthModule.Domain.Entities;
using LabsChallengeApi.Src.Shared.Application.Exceptions;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Domain.Entities;

[TestClass]
public class UserUnitTests
{
    [TestMethod]
    public void ShouldCreateUser_WhenDataIsValid()
    {
        //GIVEN
        var name = "Gian";
        var email = "gian@gmail.com";
        var password = "Password1@";
        //WHEN
        var result = User.Create(name, email, password);
        //THEN
        Assert.AreEqual(name, result.Name.Value);
        Assert.AreEqual(email, result.Email.Value);
        Assert.AreEqual(password, result.Password!.Value);
        Assert.IsFalse(result.IsEmailConfirmed);
        Assert.AreEqual(0, result.Id);
        Assert.IsNull(result.PasswordHash);
    }

    [TestMethod]
    public void ShouldRestoreUser_WhenDataIsValid()
    {
        //GIVEN
        var id = 1;
        var name = "Gian";
        var email = "gian@gmail.com";
        var passwordHash = "passwordhash";
        var isEmailConfirmed = true;
        var emailConfirmationToken = "123456";
        var emailTokenRequestedAt = DateTime.Now;
        //WHEN
        var result = User.Restore(id, name, email, passwordHash, isEmailConfirmed, emailConfirmationToken, emailTokenRequestedAt);
        //THEN
        Assert.AreEqual(id, result.Id);
        Assert.AreEqual(name, result.Name.Value);
        Assert.AreEqual(email, result.Email.Value);
        Assert.AreEqual(passwordHash, result.PasswordHash);
        Assert.AreEqual(emailConfirmationToken, result.EmailConfirmationToken);
        Assert.IsTrue(result.IsEmailConfirmed);
        Assert.IsNull(result.Password);
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenEmailIsInvalid()
    {
        //GIVEN
        var name = "Gian";
        var email = "invalidemail";
        var password = "Password1@";
        //WHEN
        var result = Assert.ThrowsException<ValidationException>(() => User.Create(name, email, password));
        //THEN
        Assert.IsTrue(result.Errors!.Contains("O email informado é inválido."));
    }

    [TestMethod]
    public void ShouldThrowValidationException_WhenPasswordIsInvalid()
    {
        //GIVEN
        var name = "Gian";
        var email = "gian@gmail.com";
        var password = "123";
        //WHEN
        var result = Assert.ThrowsException<ValidationException>(() => User.Create(name, email, password));
        //THEN
        Assert.IsTrue(result.Errors!.Contains("A senha deve ter no mínimo 8 caracteres."));
    }

    [TestMethod]
    public void ShouldSetPasswordHash()
    {
        //GIVEN
        var user = User.Create("Gian", "gian@gmail.com", "Password1@");
        var hash = "hashSenha";
        //WHEN
        user.SetPasswordHash(hash);
        //THEN
        Assert.AreEqual(hash, user.PasswordHash);
    }

    [TestMethod]
    public void ShouldSetEmailConfirmed()
    {
        //GIVEN
        var user = User.Create("Gian", "gian@gmail.com", "Password1@");
        //WHEN
        user.SetEmailConfirmed();
        //THEN
        Assert.IsTrue(user.IsEmailConfirmed);
    }
}

