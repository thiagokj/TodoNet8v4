using SecureIdentity.Password;

namespace TodoApp.Core.Contexts.AccountContext.ValueObjects;
public class Password
{
    protected Password() { }

    public Password(string? plainTextPassword = null)
    {
        if (string.IsNullOrEmpty(plainTextPassword) || string.IsNullOrWhiteSpace(plainTextPassword))
            plainTextPassword = PasswordGenerator.Generate();

        Hash = PasswordHasher.Hash(plainTextPassword);
    }

    public bool Challenge(string plainTextPassword)
        => PasswordHasher.Verify(Hash, plainTextPassword);

    public string Hash { get; } = string.Empty;
    public string ResetCode { get; } = PasswordGenerator.Generate(8, false, true);
}