using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectChallengeData.Database.Repositories.IRepositories;
using ProjectLibraryDomain.IService;
using ProjectLibraryDomain.Models;
using ProjectLibraryDomain.Models.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibraryService.Services
{
    public class LoginService : ILoginService
    {
        private static IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public LoginService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<RefreshToken> Login(LoginDTO request)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefault(a => a.Login == request.Login);

            if (user == null) { return null; };

            if (base64Encode(request.Password) != user.Password)
            {
                return null;
            }


            var token = new RefreshToken
            {
                Created = DateTime.UtcNow,
                Expires = DateTime.Now.AddDays(2),
                Token = CreateToken(user)
            };

            return token;

        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Name", user.Login),
                new Claim("Role", user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetRequiredSection("JwtToken").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public static string base64Encode(string sData) // Encode
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public static string base64Decode(string sData) //Decode
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                var decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }
    }
}
