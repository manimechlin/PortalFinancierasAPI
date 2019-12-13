using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Token
{
    public class AccessToken : JsonWebToken
    {
        public RefreshToken RefreshToken { get; private set; }
        public string value { get; set; }

        public AccessToken(string token, long expiration, RefreshToken refreshToken, string _value) : base(token, expiration)
        {
            if (refreshToken == null)
                throw new ArgumentException("Specify a valid refresh token.");

            RefreshToken = refreshToken;
            this.value = _value;
        }
    }
}
