namespace LittleConqueror.Infrastructure.JwtAdapters;

public interface ITokenManagerService
{
    void AddTokenJTI(string jti, long userId);
    void DesactivateTokenJTI(long userId);
    bool IsTokenBlacklisted(string token);
}
public class TokenManagerService : ITokenManagerService
{
    private readonly HashSet<string> _blacklistedTokenJTI = new();
    private readonly Dictionary<long, string> _activeTokenJTI = new();

    public void AddTokenJTI(string jti, long userId) => _activeTokenJTI.Add(userId, jti);

    public void DesactivateTokenJTI(long userId) => _blacklistedTokenJTI.Add(_activeTokenJTI[userId]);

    public bool IsTokenBlacklisted(string token) => _blacklistedTokenJTI.Contains(token);
}