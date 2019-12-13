using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Token
{
    public class TokenResponse
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public AccessToken Token { get; set; }

        public TokenResponse(bool success, string message, AccessToken token)
        {
            Success = success;
            Message = message;
            Token = token;
        }
    }
}
