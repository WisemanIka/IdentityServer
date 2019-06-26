using System;
using System.Collections.Generic;
using System.Linq;
using Fox.Common.Infrastructure;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager, ILogger logger, IdentityContext ctx) : base(logger, ctx)
        {
            this._userManager = userManager;
        }


        public List<User> GetUserData(List<string> userIds)
        {
            try
            {
                var ss = _userManager.Users.Where(x => userIds.Contains(x.Id)).ToList();
                return ss;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "IdentityServer.Api - UserService");
                return null;
            }
        }
    }
}
