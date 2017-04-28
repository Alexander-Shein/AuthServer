namespace AuthGuard.Services.Tokens
{
    public interface ITokensService
    {
        string Encode(TokenData tokenData);
        TokenData Decode(string token);
    }
}