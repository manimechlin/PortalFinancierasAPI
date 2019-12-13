using BusinessEntities.Token;
using BusinessEntities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(UserAuthModel user);
        RefreshToken TakeRefreshToken(string token);
        void RevokeRefreshToken(string token);
    }
}
