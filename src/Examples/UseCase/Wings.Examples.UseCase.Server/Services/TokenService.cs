using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Services
{
    public class TokenModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string Sub { get; set; }
    }
    public class TokenService
    {/// <summary>
     /// 生成JWT字符串
     /// </summary>

        // 密钥，注意不能太短
        public static string secretKey { get; set; } = "xiaomaPrincess@gmail.com";
        /// <summary>
        /// 生成JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string GetJWT(TokenModel tokenModel)
        {



            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(tokenModel, secretKey);
            Console.WriteLine(token);
            return token;
            //return "";
        }


        public static TokenModel Decode(string token)
        {

            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var json = decoder.Decode(token, secretKey, verify: true);
                return JsonSerializer.Deserialize<TokenModel>(json);
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
                return null;
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
                return null;
            }
        }

        public static TokenModel DecodeByHttpContext(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["X-Nideshop-Token"].FirstOrDefault();
            return Decode(token);
        }

    }
}
