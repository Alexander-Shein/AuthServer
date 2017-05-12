using System;
using System.Linq;

namespace AuthGuard.SL.Tokens
{
    public class TokensService : ITokensService
    {
        public string Encode(TokenData tokenData)
        {
            var time = BitConverter.GetBytes(tokenData.DateTime.ToBinary());
            var key = tokenData.Id.ToByteArray();

            var token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        public TokenData Decode(string token)
        {
            try
            {
                var tokenData = new TokenData();

                var data = Convert.FromBase64String(token);
                tokenData.DateTime = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
                tokenData.Id = new Guid(data.Skip(8).ToArray());

                return tokenData;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}