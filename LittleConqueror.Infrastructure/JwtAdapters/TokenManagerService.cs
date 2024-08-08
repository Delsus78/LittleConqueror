namespace LittleConqueror.Infrastructure.JwtAdapters;

public interface ITokenManagerService
{
    void AddTokenJTI(string jti, int userId);
    void DesactivateTokenJTI(int userId);
    bool IsTokenBlacklisted(string token);
}
public class TokenManagerService : ITokenManagerService
{
    private readonly HashSet<string> _blacklistedTokenJTI = new();
    private readonly Dictionary<int, string> _activeTokenJTI = new();

    public void AddTokenJTI(string jti, int userId) => _activeTokenJTI.Add(userId, jti);

    public void DesactivateTokenJTI(int userId) => _blacklistedTokenJTI.Add(_activeTokenJTI[userId]);

    public bool IsTokenBlacklisted(string token) => _blacklistedTokenJTI.Contains(token);
}