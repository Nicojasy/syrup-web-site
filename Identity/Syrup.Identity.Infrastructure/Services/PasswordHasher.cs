using Syrup.Identity.Application.Interfaces.Services;
using Syrup.Identity.Infrastructure.Options;
using System.Security.Cryptography;

namespace Syrup.Identity.Infrastructure.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    private const char _hashSplitter = '.';
    private const int _saltSize = 16; // 128 bit 
    private const int _keySize = 32; // 256 bit
    private static readonly HashingOptions _hashingOptions = new();

    public string Hash(string password)
    {
        using var algorithm = new Rfc2898DeriveBytes(
          password,
          _saltSize,
          _hashingOptions.Iterations,
          HashAlgorithmName.SHA512);

        var key = Convert.ToBase64String(algorithm.GetBytes(_keySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{_hashingOptions.Iterations}{_hashSplitter}{salt}{_hashSplitter}{key}";
    }

    public bool CheckPassword(string hash, string password)
    {
        var parts = hash.Split(_hashSplitter, 3);

        if (parts.Length != 3)
            return false;

        var iterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        using var algorithm = new Rfc2898DeriveBytes(
          password,
          salt,
          iterations,
          HashAlgorithmName.SHA512);

        var keyToCheck = algorithm.GetBytes(_keySize);
        return keyToCheck.SequenceEqual(key);
    }

    public bool TryNeedsUpgrade(string hash, out bool needsUpgrade)
    {
        var parts = hash.Split(_hashSplitter, 3);

        if (parts.Length != 3)
        {
            needsUpgrade = false;
            return false;
        }

        var iterations = Convert.ToInt32(parts[0]);

        needsUpgrade = iterations != _hashingOptions.Iterations;
        return true;
    }
}
