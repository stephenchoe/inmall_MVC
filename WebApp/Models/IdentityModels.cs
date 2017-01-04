using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web;

namespace WebApp.Models
{
    public class ProfileCommon : System.Web.Profile.ProfileBase
    {
        public static ProfileCommon GetProfile()
        {
            bool authenticated = HttpContext.Current.Request.IsAuthenticated;
            string userName = authenticated ? HttpContext.Current.User.Identity.Name : HttpContext.Current.Request.AnonymousID;
            return Create(userName, authenticated) as ProfileCommon;
        }

        public static ProfileCommon GetAnonymousProfile(string anonymousID)
        {
            return Create(anonymousID) as ProfileCommon;
        }

        [SettingsAllowAnonymous(true)]
        public virtual ShoppingCart ShoppingCart
        {
            get { return (ShoppingCart)this.GetPropertyValue("Cart"); }
            set { this.SetPropertyValue("Cart", value); }
        }

    }
    [Serializable]
    public class ShoppingCart
    {
        public DateTime Created;
        public DateTime LastUpdated;
        public Dictionary<string, CartItem> CartItems = new Dictionary<string, CartItem>();
    }

    [Serializable]
    public class CartItem
    {
        public CartItem(int itemId, int quantity)
        {
            Id = itemId;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    // 您可以在 ApplicationUser 類別新增更多屬性，為使用者新增設定檔資料，請造訪 http://go.microsoft.com/fwlink/?LinkID=317594 以深入了解。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            

            // 注意 authenticationType 必須符合 CookieAuthenticationOptions.AuthenticationType 中定義的項目
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在這裡新增自訂使用者宣告
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}