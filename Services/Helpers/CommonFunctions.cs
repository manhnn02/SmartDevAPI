using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DAL.Models;
using Microsoft.IdentityModel.Tokens;
using Services.Models;

namespace Services.Helpers
{
    public static class CommonFunctions
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string GenerateToken(User user, string secretKey)
        {
            var jwTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new[] {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.UserEmail),
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new Claim("ID", user.UserId.ToString()),
                            new Claim("TokenID", Guid.NewGuid().ToString())
                        }
                    ),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwTokenHandler.CreateToken(tokenDescription);
            return jwTokenHandler.WriteToken(token);
        }
    }
}
