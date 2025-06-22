using LabsChallengeApi.Src.Modules.AuthModule.Domain.VO;

namespace LabsChallengeApi.Src.Modules.AuthModule.Domain.Entities;

public class User
{
    public int Id { get; private init; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Password? Password { get; private set; }
    public string? PasswordHash { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public string EmailConfirmationToken { get; private set; }
    public DateTime EmailTokenRequestedAt { get; private set; }

    private User(
        int id,
        Name name,
        Email email,
        Password? password,
        string? passwordHash,
        bool isEmailConfirmed,
        string emailConfirmationToken,
        DateTime emailTokenRequestedAt
    )
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        PasswordHash = passwordHash;
        IsEmailConfirmed = isEmailConfirmed;
        EmailConfirmationToken = emailConfirmationToken;
        EmailTokenRequestedAt = emailTokenRequestedAt;
    }

    public void SetPasswordHash(string hash)
    {
        PasswordHash = hash;
    }

    public void SetEmailConfirmed()
    {
        IsEmailConfirmed = true;
    }

    public void SetEmailTokenRequestedAt()
    {
        EmailTokenRequestedAt = DateTime.UtcNow;
    }

    public static User Create(string name, string email, string password)
    {
        return new User(
            id: 0,
            name: new Name(name),
            email: new Email(email),
            password: new Password(password),
            passwordHash: null,
            isEmailConfirmed: false,
            emailConfirmationToken: Guid.NewGuid().ToString(),
            emailTokenRequestedAt: DateTime.UtcNow
        );
    }

    public static User Restore(
        int id,
        string name,
        string email,
        string passwordHash,
        bool isEmailConfirmed,
        string emailConfirmationToken,
        DateTime emailTokenRequestedAt
        )
    {
        return new User(
            id: id,
            name: new Name(name),
            email: new Email(email),
            password: null,
            passwordHash: passwordHash,
            isEmailConfirmed: isEmailConfirmed,
            emailConfirmationToken: emailConfirmationToken,
            emailTokenRequestedAt: emailTokenRequestedAt
        );
    }
}
