namespace AuthGuard.SL.Tokens
{
    public interface ITokensService
    {
        string Encode(TokenData tokenData);
        TokenData Decode(string token);
    }
}