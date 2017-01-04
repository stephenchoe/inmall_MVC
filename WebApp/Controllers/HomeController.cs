using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

using WebApp.Models;
using BLL;
using Model;
using System.Configuration;
using Core.Extensions;
using Web.Core;
using Web.Core.Models;
using System.Web.Profile;


namespace WebApp.Controllers
{
    public class HomeController : AppControllerBase
    {

        private readonly IADService adService;
        private readonly ICategoryService categoryService;
        public HomeController(IErrorLogService errorLogService, IADService adService, ICategoryService categoryService)
            : base(errorLogService)
        {

            this.adService = adService;
            this.categoryService = categoryService;
        }
        public ActionResult Test()
        {
           
            return View();
        }
        public ActionResult Index()
        {
            try
            {
                var adList = adService.GetAds();
                var topCategories = categoryService.GetRandonTopCategories(6);
                var model = new HomeIndexViewModel(adList, topCategories);

                return View(model);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return ErrorPage();
            }
          
        }

        FileContentResult DefaultADImage()
        {
            var image = Image.FromFile(Server.MapPath("~/Content/images/defaultAD.png"));
            byte[] imageFile = ImageHelper.ConvertImageToByteArray(image, "png");
            return new FileContentResult(imageFile, "image/png");
        }
        public FileContentResult ADImage(int id)
        {
            try
            {
                var ad = adService.GetById(id);
                if (ad == null) return DefaultADImage();
                if (ad.ImageFile == null) return DefaultADImage();
                return new FileContentResult(ad.ImageFile, "image/jpeg");
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return DefaultADImage();
            }
        }
        public ActionResult About()            
        {
            //ShoppingCart bookCart = new ShoppingCart();
            //bookCart.CartItems.Add("Contoso", new CartItem(37843, "Widget", 49.99));
            //bookCart.CartItems.Add("Microsoft", new
            //    CartItem(39232, "Software", 49.99));
            //bookCart.CartItems.Add("davc", new
            //    CartItem(232434, "Software", 12.99));

            //Profile["Cart"] = bookCart;
            //Profile.Save();

            //Profile["Cart"] 
            var profile = ProfileCommon.GetProfile();
            //profile.ShoppingCartCart = bookCart;
            //profile.Save();

            var count = profile.ShoppingCart.CartItems.Count;

            //ProfileCommon profile = ProfileCommon.GetProfile();
            //profile.Cart = bookCart;

            //profile.Save();
            
            //ProfileCommon profile = ProfileCommon.GetProfile();
            //profile.Cart = bookCart;

            //profile.Save();




            //var profile = ProfileCommon.GetProfile();


            //profile.Cart = bookCart;

            //profile.Save();

            //if (Profile.IsAnonymous)
            //{
            //    Profile. = 10;
            //    Profile.Save();
            //}
           
            //if (User.Identity.IsAuthenticated)
            //    ViewBag.Message = "User = " + User.Identity.Name;
            //else
            //    ViewBag.Message = "AnonymousID = " + Request.AnonymousID;

            return View();

            
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}