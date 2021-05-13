using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Sampan.Common.Util;

namespace Sampan.WebExtension.Authentication
{
    public static class JWTService
    {
        public static string GetToken(List<Claim> claims)
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Appsettings.app("JWT", "Secret")));
            var creds = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var issuer = Appsettings.app("JWT", "Issuer");
            var audience = Appsettings.app("JWT", "Audience");

            #region ------------------注解--------------------------

            /**
                * Claims (Payload)
                   Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                   iss: The issuer of the token，token 是给谁的
                   sub: The subject of the token，token 主题
                   exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                   iat: Issued At。 token 创建时间， Unix 时间戳格式
                   jti: JWT ID。针对当前 token 的唯一标识
                   除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                * */

            #endregion

            //int.TryParse(Appsettings.app(new string[] { "Expires" }), out int _expires);
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddDays(7), //有效期
                signingCredentials: creds);
            ;
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }
    }
}