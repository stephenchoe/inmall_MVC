using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;

using Model;
using Microsoft.Owin;



namespace BLL
{
    public class AppSignInManager : SignInManager<AppUser, string>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            this.AppUserManager = userManager;
        }
        private AppUserManager AppUserManager { get; set; }
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
        {
            return user.GenerateUserIdentityAsync((AppUserManager)UserManager);
        }

        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        {
            return new AppSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }
        public async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool shouldLockout = true)
        {

            var user = await UserManager.FindByNameAsync(userName);
            if (user == null) return SignInStatus.Failure;

            string userId = user.Id;

            var locked = await UserManager.IsLockedOutAsync(userId);
            if (locked) return SignInStatus.LockedOut;

            var passwordValid = await UserManager.CheckPasswordAsync(user, password);
            if (!passwordValid)  //密碼錯誤
            {
                if (shouldLockout)
                {
                    await UserManager.AccessFailedAsync(userId);
                    locked = await UserManager.IsLockedOutAsync(userId);
                    if (locked) return SignInStatus.LockedOut;
                }

                return SignInStatus.Failure;
            }

            var emailConfirmed = await UserManager.IsEmailConfirmedAsync(userId);
            if (!emailConfirmed) return SignInStatus.RequiresVerification;

            var status = await SignIn(user);
            return status;


        }

        private async Task<SignInStatus> SignIn(AppUser user)
        {
            await UserManager.ResetAccessFailedCountAsync(user.Id);

            var userIdentity = await user.GenerateUserIdentityAsync(this.AppUserManager);
           
            AuthenticationManager.SignIn(userIdentity);

            return SignInStatus.Success;

        }
        public  async Task<SignInStatus> ReSign(AppUser user)
        {
            AuthenticationManager.SignOut();

            var status = await SignIn(user);
            return status;
        }
    }
}
