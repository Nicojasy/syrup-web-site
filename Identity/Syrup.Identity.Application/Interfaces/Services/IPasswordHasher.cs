namespace Syrup.Identity.Application.Interfaces.Services;

public interface IPasswordHasher
{
    string Hash(string password);
    bool CheckPassword(string hash, string password);
    bool TryNeedsUpgrade(string hash, out bool needsUpgrade);
}
