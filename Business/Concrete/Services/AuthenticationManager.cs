using AutoMapper;
using Business.Abstract.Services;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Business.Concrete.Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _manager;
        private readonly IConfiguration _configuration;

        private User? _user;

        public AuthenticationManager(IMapper mapper, UserManager<User> manager, IConfiguration configuration)
        {
            _mapper = mapper;
            _manager = manager;
            _configuration = configuration;
        }

        public async Task<TokenDto> CreateToken(bool poulateExp)
        {
            var signinCredentials = GetSigninCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;


            if (poulateExp == true)
            {
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            }
            await _manager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }



        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var user = _mapper.Map<User>(userForRegistrationDto);
            var result = await _manager.CreateAsync(user, userForRegistrationDto.Password);

            if (result == IdentityResult.Success)
            {
                await _manager.AddToRolesAsync(user, userForRegistrationDto.Roles);
            }
            return result;

        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto)
        {
            _user = await _manager.FindByNameAsync(userForAuthenticationDto.UserName);
            var result = (_user != null && await _manager.CheckPasswordAsync(_user, userForAuthenticationDto.Password));
            return result;
        }

        private SigningCredentials GetSigninCredentials()
        {
            var jwtSetting = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSetting["secretKey"]);
            var secret = new SymmetricSecurityKey(secretKey);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
           {
               new Claim(ClaimTypes.Name,_user.UserName)
           };
            var roles = await _manager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials, List<Claim> claims)
        {
            var jwtSetting = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSetting["validIssuer"],
                audience: jwtSetting["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSetting["expires"])),
                signingCredentials: signinCredentials);

            return tokenOptions;

        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(String token)
        {
            var settings = _configuration.GetSection("JwtSettings");
            var secretKey = settings["secretKey"];

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = settings["validIssuer"],
                ValidAudience = settings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }

            return principal;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _manager.FindByNameAsync(principal.Identity.Name);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now || user.RefreshToken != tokenDto.RefreshToken)
            {
                throw new Exception();
            }
            _user = user;

            return await CreateToken(false);
        }
    }
}