using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

using Model;
using DAL;

namespace BLL
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        {

        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext owinContext)
        {
            var manager = new AppUserManager(new UserStore<AppUser>(owinContext.Get<InmallContext>()));

            // 設定使用者名稱的驗證邏輯
            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 設定密碼的驗證邏輯
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireUppercase = true,
            };

            // 設定使用者鎖定詳細資料
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromHours(24);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;


            // 註冊雙因素驗證提供者。此應用程式使用手機和電子郵件接收驗證碼以驗證使用者
            // 您可以撰寫專屬提供者，並將它外掛到這裡。

            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<AppUser>()
                {
                    MessageFormat = "您的驗證碼是："
                });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<AppUser>()
            {

                Subject = "您的驗證信",
                BodyFormat = "您的驗證碼是：{0}"
            });

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("Inmall AppUser Identity"))
                    {
                        TokenLifespan = TimeSpan.FromHours(24)

                    };
            }

            return manager;

        }



    }


}
