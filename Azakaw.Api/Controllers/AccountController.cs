using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azakaw.Api.Models;
using Azakaw.Domain;
using Azakaw.Domain.Config;
using Azakaw.Domain.Models;
using Azakaw.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Azakaw.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly SecurityConfig _securityConfig;

        public AccountController(IUserService userService, IOptions<SecurityConfig> securityConfig)
        {
            _userService = userService;
            _securityConfig = securityConfig.Value;
        }

        [HttpPost("SignIn"), AllowAnonymous]
        public ActionResult<ResponseModel<string>> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid) return ResponseModel<string>.GetValidationFailureResponse(null);

            var response = _userService.GetByEmail(model.Email);

            if (!response.Status)
                return ResponseModel<string>.GetNotFoundResponse();

            if (response.Data.Password != model.Password)
                return ResponseModel<string>.GetValidationFailureResponse(null);

            var user = response.Data;
            var tokenString = GetTokenString(user);
            WorkContext.Authorized(user, tokenString);

            return ResponseModel<string>.GetSuccessResponse(tokenString);
        }

        private string GetTokenString(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_securityConfig.JwtSecret ?? "RWh0aXNoYW1BaG1lZFNpZGRpcXVpKzkyMzQ1NjQzNDM0Ng==");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(_securityConfig.ExpiresInHours ?? 168), // 7*24=168
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return $"Bearer {tokenString}";
        }
    }
}