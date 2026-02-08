using System.Security.Cryptography;

namespace App.Modules.Sys.Infrastructure.Identity;

/// <summary>
/// PBKDF2 password hasher with per-password salt.
/// 
/// Hash format: Base64({ version_byte, salt[16], subkey[32] })
/// - Version byte: 0x01 for PBKDF2-SHA256
/// - Salt: 16 random bytes (unique per password)
/// - Subkey: 32 bytes derived from password + salt
/// 
/// This is compatible with ASP.NET Core Identity V3 format.
/// </summary>
public class Pbkdf2PasswordHasher : IPasswordHasher
{
    // Version identifier for hash format
    private const byte FormatMarker = 0x01;

    // Salt size in bytes (128 bits)
    private const int SaltSize = 16;

    // Subkey (derived key) size in bytes (256 bits)
    private const int SubkeySize = 32;

    // PBKDF2 iteration count
    // OWASP recommends 310,000 for SHA256 as of 2023
    // Using 100,000 as a balance between security and performance
    private const int IterationCount = 100_000;

    /// <inheritdoc />
    public string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        // Generate random salt
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        // Derive subkey using PBKDF2-SHA256
        byte[] subkey = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            IterationCount,
            HashAlgorithmName.SHA256,
            SubkeySize);

        // Combine: version + salt + subkey
        byte[] outputBytes = new byte[1 + SaltSize + SubkeySize];
        outputBytes[0] = FormatMarker;
        Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
        Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, SubkeySize);

        return Convert.ToBase64String(outputBytes);
    }

    /// <inheritdoc />
    public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
    {
        if (string.IsNullOrEmpty(hashedPassword))
        {
            return PasswordVerificationResult.Failed;
        }

        if (string.IsNullOrEmpty(providedPassword))
        {
            return PasswordVerificationResult.Failed;
        }

        byte[] decodedHashedPassword;
        try
        {
            decodedHashedPassword = Convert.FromBase64String(hashedPassword);
        }
        catch
        {
            return PasswordVerificationResult.Failed;
        }

        // Verify format
        if (decodedHashedPassword.Length != 1 + SaltSize + SubkeySize)
        {
            return PasswordVerificationResult.Failed;
        }

        if (decodedHashedPassword[0] != FormatMarker)
        {
            return PasswordVerificationResult.Failed;
        }

        // Extract salt
        byte[] salt = new byte[SaltSize];
        Buffer.BlockCopy(decodedHashedPassword, 1, salt, 0, SaltSize);

        // Extract expected subkey
        byte[] expectedSubkey = new byte[SubkeySize];
        Buffer.BlockCopy(decodedHashedPassword, 1 + SaltSize, expectedSubkey, 0, SubkeySize);

        // Derive subkey from provided password
        byte[] actualSubkey = Rfc2898DeriveBytes.Pbkdf2(
            providedPassword,
            salt,
            IterationCount,
            HashAlgorithmName.SHA256,
            SubkeySize);

        // Time-constant comparison
        if (CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey))
        {
            return PasswordVerificationResult.Success;
        }

        return PasswordVerificationResult.Failed;
    }
}
