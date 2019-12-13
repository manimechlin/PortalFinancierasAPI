using BusinessEntities.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth
{
    public interface IAuthenticationService
    {
        TokenResponse CreateAccessTokenAsync(string email, string password);

        TokenResponse RefreshTokenAsync(string refreshToken, string userEmail);

        void RevokeRefreshToken(string refreshToken);

    }
}
