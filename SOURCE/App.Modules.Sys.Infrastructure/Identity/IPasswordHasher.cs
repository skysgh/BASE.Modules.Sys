using App.Modules.Sys.Domain.Identity;

namespace App.Modules.Sys.Infrastructure.Identity;

/// <summary>
/// Service for hashing and verifying passwords.
/// 
/// Uses PBKDF2 with:
/// - HMAC-SHA256
/// - 128-bit (16 byte) salt unique per hash
/// - 256-bit (32 byte) subkey
/// - 10,000+ iterations
/// 
/// Salt is embedded in the hash output, so each password has its own unique salt.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hash a password.
    /// </summary>
    /// <param name="password">The plain text password.</param>
    /// <returns>The hashed password with embedded salt.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verify a password against a hash.
    /// </summary>
    /// <param name="hashedPassword">The stored hash.</param>
    /// <param name="providedPassword">The password to verify.</param>
    /// <returns>Result indicating success, failure, or need to rehash.</returns>
    PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword);
}

/// <summary>
/// Result of password verification.
/// </summary>
public enum PasswordVerificationResult
{
    /// <summary>
    /// Password does not match.
    /// </summary>
    Failed = 0,

    /// <summary>
    /// Password matches.
    /// </summary>
    Success = 1,

    /// <summary>
    /// Password matches but hash should be upgraded (e.g., more iterations).
    /// </summary>
    SuccessRehashNeeded = 2
}
