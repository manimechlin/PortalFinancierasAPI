using Services.Hashing;
using BusinessEntities.Token;
using BusinessEntities.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Services.Helper;
using Newtonsoft.Json;

namespace Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();

        private readonly TokenOptions _tokenOptions;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly IPasswordHasher _passwordHaser;

        private string _appkey;
        private string _appiv;

        private MixData _mixData = null;

        IConfiguration _config;
        public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, SigningConfigurations signingConfigurations, IPasswordHasher passwordHaser, IConfiguration config)
        {
            _passwordHaser = passwordHaser;
            _tokenOptions = tokenOptionsSnapshot.Value;
            _signingConfigurations = signingConfigurations;
            _config = config;
            _appkey = config.GetSection("appSettings").GetSection("AppKey").Value;
            _appiv = config.GetSection("appSettings").GetSection("AppIv").Value;
        }

        public AccessToken CreateAccessToken(UserAuthModel user)
        {
            var refreshToken = BuildRefreshToken(user);
            var accessToken = BuildAccessToken(user, refreshToken);
            _refreshTokens.Add(refreshToken);

            return accessToken;
        }

        public RefreshToken TakeRefreshToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var refreshToken = _refreshTokens.SingleOrDefault(t => t.Token == token);
            if (refreshToken != null)
                _refreshTokens.Remove(refreshToken);

            return refreshToken;
        }

        public void RevokeRefreshToken(string token)
        {
            TakeRefreshToken(token);
        }

        private RefreshToken BuildRefreshToken(UserAuthModel user)
        {
            var refreshToken = new RefreshToken
            (
                token: _passwordHaser.HashPassword(Guid.NewGuid().ToString()),
                expiration: DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks
            );

            return refreshToken;
        }

        private AccessToken BuildAccessToken(UserAuthModel user, RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

            var securityToken = new JwtSecurityToken
            (
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: GetClaims(user),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: _signingConfigurations.SigningCredentials
            );


            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);
            //var readed = handler.ReadJwtToken(accessToken);

            string value = securityToken.Claims.FirstOrDefault(claimRecord => claimRecord.Type == "nameid").Value.ToString();

            return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken, value);
        }

        private IEnumerable<Claim> GetClaims(UserAuthModel user)
        {
            //RtCrypto crypto = new RtCrypto(_config.GetSection("Crypto").GetSection("Key").Value, _config.GetSection("Crypto").GetSection("Vector").Value);
            _mixData = new MixData();

            user.userid = user.userid != "" ? _mixData.E(user.userid) : "";
            user.password = null;

            string json = JsonConvert.SerializeObject(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.email),
                new Claim(JwtRegisteredClaimNames.NameId, cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt, _appkey, _appiv))
            };

            //foreach (var userRole in user.UserRoles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            //}

            return claims;
        }
    }
}
