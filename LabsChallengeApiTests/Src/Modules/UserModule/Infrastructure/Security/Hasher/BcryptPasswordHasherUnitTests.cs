using LabsChallengeApi.Src.Shared.Infrastructure.Security.Hasher;
using LabsChallengeApi.Src.Shared.Infrastructure.Security.Hasher.Adapters;

namespace LabsChallengeApiTests.Src.Modules.UserModule.Infrastructure.Security.Hasher;

[TestClass]
public class BcryptPasswordHasherUnitTests
{
    private IPasswordHasher _passwordHasher = null!;

    [TestInitialize]
    public void Setup()
    {
        _passwordHasher = new BcryptPasswordHasher();
    }

    [TestMethod]
    public void Hash_ShouldGenerateDifferentHash_ForSamePassword()
    {
        //GIVEN
        var password = "Test@123";
        //WHEN
        var hash1 = _passwordHasher.Hash(password);
        var hash2 = _passwordHasher.Hash(password);
        //THEN
        Assert.AreNotEqual(hash1, hash2, "Hashes should be different due to salt.");
    }

    [TestMethod]
    public void Verify_ShouldReturnTrue_WhenPasswordMatchesHash()
    {
        //GIVEN
        var password = "Test@123";
        var hash = _passwordHasher.Hash(password);
        //WHEN
        var result = _passwordHasher.Verify(password, hash);
        //THEN
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Verify_ShouldReturnFalse_WhenPasswordDoesNotMatchHash()
    {
        // GIVEN
        var password = "Test@123";
        var hash = _passwordHasher.Hash(password);
        // WHEN
        var result = _passwordHasher.Verify("WrongPassword", hash);
        // THEN
        Assert.IsFalse(result);
    }
}
