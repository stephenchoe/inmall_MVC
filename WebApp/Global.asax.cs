using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using System.Configuration;
using System.Web.Profile;
using WebApp.Models;
using System.Web.Security;

using DAL;
using Model;
using Core.Extensions;


namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        public void Profile_OnMigrateAnonymous(object sender, ProfileMigrateEventArgs args)
        {
            string anonymousID = args.AnonymousID;
            var anonymousProfile = ProfileCommon.GetAnonymousProfile(anonymousID);
            if (anonymousProfile == null) return;

          
            var cart = anonymousProfile.ShoppingCart;
            if (cart != null && cart.CartItems.Count > 0)
            {
                MigrateCart(cart);
            }
           
            // Delete the anonymous profile. If the anonymous ID is not 
            // needed in the rest of the site, remove the anonymous cookie.

            ProfileManager.DeleteProfile(anonymousID);
            AnonymousIdentificationModule.ClearAnonymousIdentifier();

            // Delete the user row that was created for the anonymous user.
            //  Membership.DeleteUser(anonymousID, true);
        }

        void MigrateCart(ShoppingCart cart)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            var keys = cart.CartItems.Keys.ToList();
            using (var context = new InmallContext())
            {
                foreach (var key in keys)
                {
                    var item = cart.CartItems[key];
                    int pid = item.Id;
                    int quantity = item.Quantity;
                    var newItem = new Model.CartItem
                    {
                        CartId = userName,
                        ProductId = pid,
                        Quantity = quantity,
                        CreateDate = DateTime.Now.ConvertToTaipeiTime(),
                        LastUpdate = DateTime.Now.ConvertToTaipeiTime()
                    };
                    context.ShoppingCartItems.Add(newItem);
                }
                context.SaveChanges();
            }
        } 
    }
}
