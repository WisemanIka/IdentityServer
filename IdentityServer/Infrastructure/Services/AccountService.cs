using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager,
            ILogger logger, IdentityContext ctx) : base(logger, ctx)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }

        public async Task<RegistrationResponse> Registration(RegistrationRequest request)
        {
            try
            {
                var userDbModel = request.Map<RegistrationRequest, User>();
                var result = await _userManager.CreateAsync(userDbModel, request.Password);

                if (result.Succeeded)
                {
                    var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(userDbModel);

                    await _userManager.ConfirmEmailAsync(userDbModel, emailToken);
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
                Logger.LogException(ex, "Bom.Identity.Api - AccountService Registration");
                return null;
            }
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                var response = new LoginResponse();
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(request.Email);
                }
                return response;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Bom.Identity.Api - AccountService Login");
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
                Logger.LogException(ex, "Bom.Identity.Api - AccountService CheckEmailExistence");
                return true;
            }
        }
    }
}
