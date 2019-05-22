using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TutorialMoya001.Models;

namespace TutorialMoya001.Utils
{
    public class Util
    {
        string a;
        string b;
        string c;
        public Util(string a, string b, string c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public Util()
        {

        }

        public string GetToken(User user)
        {
            var claims = new[]
            {
                new Claim("UserData", JsonConvert.SerializeObject(user))
            };
            var token = new JwtSecurityToken
            (
                issuer: a,
                audience: b,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(c)),
                SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public object GenerateBase64StringByGUID(Guid guid)
        {
            var a = Convert.ToBase64String(guid.ToByteArray());
            return a;
        }
    }
}
