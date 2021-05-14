using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sampan.Common.Util;

namespace Sampan.WebExtension.Dependency
{
    public static class JWTAuthenticationDependency
    {
        public static void AddJWTAuthentication(this IServiceCollection services)
        {
            var Issuer = Appsettings.app("JWT", "Issuer");
            var Audience = Appsettings.app("JWT", "Audience");
            var Secret = Appsettings.app("JWT", "Secret");

            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = !string.IsNullOrEmpty(Issuer), //是否验证Issuer
                ValidateAudience = true, //是否验证Audience
                ValidateLifetime = true, //是否验证失效时间
                ValidateIssuerSigningKey = true, //是否验证SecurityKey
                ValidAudience = Audience, //Audience
                ValidIssuer = Issuer, //Issuer，这两项和前面签发jwt的设置一致
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)), //拿到SecurityKey
                //AudienceValidator = (m, n, z) =>
                // {
                //     return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
                // },//自定义校验规则，可以新登录后将之前的无效
            };

            //jwt校验
            services.AddAuthentication(o =>
                {
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => { options.TokenValidationParameters = tokenValidationParameters; });
        }
    }
}