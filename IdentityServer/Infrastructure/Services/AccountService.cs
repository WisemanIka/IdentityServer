using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Providers.EmailSender;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Infrastructure
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender,
            ILogger logger, IdentityContext ctx) : base(logger, ctx)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailSender = emailSender;
        }

        public async Task<RegistrationResponse> Registration(RegistrationRequest request)
        {
            try
            {
                var userDbModel = request.Map<RegistrationRequest, User>();
                var result = await _userManager.CreateAsync(userDbModel, request.Password);

                if (result.Succeeded)
                {
                    var emailToken = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(userDbModel));

                    var baseUrl = MyHttpContext.AppBaseUrl;

                    var confirmEmailCallBack = string.Format(baseUrl + "/api/Account/ConfirmEmail?userId={0}&token={1}", userDbModel.Id, emailToken);

                    var emailBody = "გთხოვთ დაადასტუროთ ემაილი დააჭირეთ <a href='" + confirmEmailCallBack + "'>აქ</a>";

                    await _emailSender.SendEmailAsync(userDbModel.Email, "Email Confirmation", emailBody);
                }

                return new RegistrationResponse
                {
                    Success = result.Succeeded,
                    Errors = result.Errors.Any() ? result.Errors.Select(x => x.Description).ToList() : new List<string>(),
                    Email = userDbModel.Email
                };

            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "IdentityServer.Api - AccountService Registration");
                return null;
            }
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null) return new LoginResponse();

                var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, lockoutOnFailure: true);

                if (!result.Succeeded || !user.EmailConfirmed) return new LoginResponse();

                var response = new LoginResponse
                {
                    Succeeded = result.Succeeded && user.EmailConfirmed,
                    Token = GenerateToken(user)
                };

                return response;

            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "IdentityServer.Api - AccountService Login");
                return null;
            }
        }

        public async Task<bool> CheckEmailExistence(string email)
        {
            try
            {
                var result = await _userManager.FindByEmailAsync(email);
                return result != null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "IdentityServer.Api - AccountService CheckEmailExistence");
                return false;
            }
        }

        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                var result = await _userManager.ConfirmEmailAsync(user, token);

                return result.Succeeded;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "IdentityServer.Api - AccountService CheckEmailExistence");
                return false;
            }
            
        }

        private string GenerateToken(User user)
        {
            //var issuer = "http://www.fox.ge";
            var identityClaims = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Firstname),
                new Claim(ClaimTypes.Surname, user.Lastname)
            });

            //foreach (var claim in claims)
            //{
            //    identityClaims.AddClaim(new Claim(claim.Type, claim.Value));
            //}

            var secretKey = Encoding.ASCII.GetBytes("xd3RCPJXoWwYJiA7");
            var credentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(createToken);

            return token;
        }
    }
}
