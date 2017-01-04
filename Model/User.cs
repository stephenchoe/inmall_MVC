using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Model
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }

        //出生日期
        public DateTime DOB { get; set; }

        public Address Address { get; set; }
        public DateTime CreateDate { get; set; }

        public string FullName
        {
            get
            {
                return LastName + FirstName;
            }
        }
        public string GenderText
        {
            get
            {
                string genderText = Gender ? "先生" : "小姐";
                return genderText;
            }
        }

        public string NickName
        {
            get
            {
                return String.Format("{0} {1}", FullName, GenderText);
            }
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // 注意 authenticationType 必須符合 CookieAuthenticationOptions.AuthenticationType 中定義的項目
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在這裡新增自訂使用者宣告
            userIdentity.AddClaim(new Claim("UserId", Id));
            userIdentity.AddClaim(new Claim("NickName", NickName));

            return userIdentity;
        }

    }

 
    public class AppRole : IdentityRole
    {
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
        public int Order { get; set; }
    }



}
