using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Demo.MS
{
    public class TokenHelper
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetToken(UserDto user)
        {
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Name),
                    new Claim("Role", "admin")
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: Const.Domain,
                audience: Const.Domain,
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds);
            return $"Bearer {new JwtSecurityTokenHandler().WriteToken(token) }";
        }
    }

    public class UserDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
    }

    public class Const
    {
        /// <summary>
        /// Key
        /// </summary>
        public const string SecurityKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDI2a2EJ7m872v0afyoSDJT2o1+SitIeJSWtLJU8/Wz2m7gStexajkeD+Lka6DSTy8gt9UwfgVQo6uKjVLG5Ex7PiGOODVqAEghBuS7JzIYU5RvI543nNDAPfnJsas96mSA7L/mD7RTE2drj6hf3oZjJpMPZUQI/B1Qjb5H3K3PNwIDAQAB";
        public const string Domain = "http://localhost:5000";
    }
}
