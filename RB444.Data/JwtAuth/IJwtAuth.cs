namespace RB444.Data.JwtAuth
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
        string RefreshToken(string username);
    }
}
