using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SplitWise.Business.Interface;
using SplitWise.DataAccess.Interface;
using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SplitWise.Business
{
    public class LoginService :ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;

        public LoginService(ILoginRepository loginRepository,IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }
        public async Task<SplitUser> userFindByEmail(string email)
        {
            if (email == null) throw new ArgumentNullException("email");
            else
            {
                return await _loginRepository.userFindByEmail(email);
            }

        }
        public async Task<IdentityResult> Createuser(SplitUser identityUser, string password)
        {
           return await _loginRepository.userCreate(identityUser, password);
        }
        public async Task<RegisterRequest> GetResponseRegister(SplitUser user, Register register)
        {
            string usid = await _loginRepository.userGetUserId(user);
            RegisterRequest responseRegister = new RegisterRequest()
            {
                UserId = usid,
                UserName = register.UserName,
                Email = register.Email,
            };
            return responseRegister;
        }
        private JwtSecurityToken GenerateToken(List<Claim> claimss)
        {


            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claimss,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return token;
        }
        public TokenResponse GetTokenResponse(SplitUser user)
        {

            var authclaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var JWTtoken = GenerateToken(authclaims);

            Profiles profile = new Profiles()
            {
                UId = user.Id,
                Email = user.Email,
                Name = user.UserName,
            };

            TokenResponse response = new TokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(JWTtoken),
                Profiles = profile
            };
            return response;
        }
        public async Task<bool> userCheckPassword(SplitUser users, string password)
        {
            return await _loginRepository.userCheckPassword(users, password);
        }
        public async Task<List<GetUsers>> GetGetUsers()
        {
            var users = await _loginRepository.GetUsersList();

            List<GetUsers> UserDetails = new List<GetUsers>();

            foreach (var user in users)
            {
                GetUsers v = new GetUsers()
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserName = user.UserName
                };
                UserDetails.Add(v);
            }
            Userss userss = new Userss()
            {
                Userrs = UserDetails
            };
            return UserDetails;
        }


    }
}
