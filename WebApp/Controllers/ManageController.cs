using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Xml; 

using Model;
using BLL;
using WebApp.Models;
using Web.Core.Models;
using System.Xml.Linq;

namespace WebApp.Controllers
{
    [Authorize]
    public class ManageController : AppUserController
    {
     
        public ManageController(IErrorLogService errorLogService, IEmailService emailService, IAddressService addressService)
            : base(errorLogService, emailService, addressService)
        {
          

        }

       


        [HttpGet]
        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            StatusAlertType alertType = StatusAlertType.Error;
            string errorTitle = "變更密碼失敗";
            string errorMsg = "系統暫時無回應，請稍後再試。";
            bool showConfirm = true;
            string successTitle = "變更密碼成功";
            string successMsg = "";

            var changePasswordResult = ChangePasswordResult.Failure;
            try
            {
                string oldPassword = model.OldPassword.Trim();
                string password = model.Password.Trim();
                string userId = User.Identity.GetUserId();
                var result = await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.Password);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    alertType = StatusAlertType.Success;
                    changePasswordResult = ChangePasswordResult.Success;

                    model.ResultStatus = GetChangePasswordResultModel(alertType, successTitle, successMsg, showConfirm, changePasswordResult);
                    return PartialView(model);
                }

                //變更失敗
                alertType = StatusAlertType.Error;
                changePasswordResult = ChangePasswordResult.Failure;
                errorMsg = result.Errors.FirstOrDefault(); ;

                model.ResultStatus = GetChangePasswordResultModel(alertType, errorTitle, errorMsg, showConfirm, changePasswordResult);
                return PartialView(model);

            }
            catch (Exception ex)
            {
                HandleException(ex);

                alertType = StatusAlertType.Error;
                changePasswordResult = ChangePasswordResult.Exception;
                errorMsg = "系統暫時無回應，請稍後再試。";

                model.ResultStatus = GetChangePasswordResultModel(alertType, errorTitle, errorMsg, showConfirm, changePasswordResult);
                return PartialView(model);
            }
        }
        ChangePasswordResultModel GetChangePasswordResultModel(StatusAlertType type, string title, string msg, bool showConfirm, ChangePasswordResult changePasswordResult)
        {
            var model = new ChangePasswordResultModel() { ChangePasswordResult = changePasswordResult };
            SetStatusAlert(model, type, title, msg, showConfirm);

            return model;
        }


        [HttpGet]
        public ActionResult Profiles()
        {
            string userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            var model = new ProfilesUpdateModel()
            {
                Email=user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CityId = user.Address.CityId,
                DistrictId = user.Address.DistrictId,
                DOB = user.DOB,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                StreetAddress = user.Address.StreetAddress,

            };

            LoadOptions(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Profiles(ProfilesUpdateModel model)
        {
            StatusAlertType alertType = StatusAlertType.Error;
            string errorTitle = "更新會員資料失敗";
            string errorMsg = "系統暫時無回應，請稍後再試。";
            bool showConfirm = true;
            string successTitle = "更新會員資料成功";
            string successMsg = "";

            var profilesUpdateResult = ProfilesUpdateResult.Failure;
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadOptions(model);
                    return PartialView(model);
                }
                var user = FindUserByEmail(model.Email , throwException:true);

                user.FirstName = model.FirstName.Trim();
                user.LastName = model.LastName.Trim();
                user.Address.StreetAddress = model.StreetAddress;
                user.Address.CityId = model.CityId;
                user.Address.DistrictId = model.DistrictId;
                user.Gender = model.Gender;
                user.DOB = Convert.ToDateTime(model.DOB);
             
                var result= await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    alertType = StatusAlertType.Success;
                    profilesUpdateResult = ProfilesUpdateResult.Success;

                    model.ResultStatus = GetProfilesUpdateResultModel(alertType, successTitle, successMsg, showConfirm, profilesUpdateResult);

                    LoadOptions(model);
                    return PartialView(model);
                }

                //變更失敗
                alertType = StatusAlertType.Error;
                profilesUpdateResult = ProfilesUpdateResult.Failure;
                errorMsg = result.Errors.FirstOrDefault(); ;

                model.ResultStatus = GetProfilesUpdateResultModel(alertType, errorTitle, errorMsg, showConfirm, profilesUpdateResult);

                LoadOptions(model);
                return PartialView(model);

            }
            catch (Exception ex)
            {
                HandleException(ex);

                alertType = StatusAlertType.Error;
                profilesUpdateResult = ProfilesUpdateResult.Exception;
                errorMsg = "系統暫時無回應，請稍後再試。";

                model.ResultStatus = GetProfilesUpdateResultModel(alertType, errorTitle, errorMsg, showConfirm, profilesUpdateResult);

                LoadOptions(model);
                return PartialView(model);
            }
        }




        void LoadOptions(ProfilesViewModel model)
        {
            model.CityOptions = CityOptions(model.CityId);

            if (model.CityId < 1)
            {
                var first = model.CityOptions.FirstOrDefault();
                model.CityId = Convert.ToInt32(first.Value);
            }

            model.DistrictOptions = DistrictOptions(model.CityId, model.DistrictId);
        }
        ProfilesUpdateResultModel GetProfilesUpdateResultModel(StatusAlertType type, string title, string msg, bool showConfirm, ProfilesUpdateResult result)
        {
            var model = new ProfilesUpdateResultModel() { ProfilesUpdateResult = result };
            SetStatusAlert(model, type, title, msg, showConfirm);

            return model;
        }
        [AllowAnonymous]
        public ActionResult Test()
        {
            NewXDocument();
            ViewBag.test = "done";
            return View();
        }

        void NewXDocument()
        { 
           XNamespace xmlns = XNamespace.Get("http://mvcsitemap.codeplex.com/schemas/MvcSiteMap-File-4.0");
        XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
        XNamespace schemaLocation = XNamespace.Get("http://mvcsitemap.codeplex.com/schemas/MvcSiteMap-File-4.0 MvcSiteMapSchema.xsd");

          var doc= new XDocument(
            new XElement(xmlns + "mvcSiteMap",
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "schemaLocation", schemaLocation)));

          doc.Save(System.IO.Path.Combine(Server.MapPath("~"), "testMvc.sitemap"));
        }

        void CreateSiteMap()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("mvcSiteMap");
            root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            root.SetAttribute("xmlns", "http://mvcsitemap.codeplex.com/schemas/MvcSiteMap-File-4.0");
            root.SetAttribute("xsi:schemaLocation", "http://mvcsitemap.codeplex.com/schemas/MvcSiteMap-File-4.0 MvcSiteMapSchema.xsd");
            doc.AppendChild(root);

            XmlDeclaration xmldecl;
            xmldecl = doc.CreateXmlDeclaration("1.0", null, null);
            xmldecl.Encoding = "UTF-8";

            doc.InsertBefore(xmldecl, root);

            XmlElement home = doc.CreateElement("mvcSiteMapNode");

            home.SetAttribute("title", "花蓮原鄉");
            home.SetAttribute("controller", "Home");
            home.SetAttribute("action", "Index");

            XmlElement product = doc.CreateElement("mvcSiteMapNode");
            product.SetAttribute("title", "各式包包/皮夾");
            product.SetAttribute("controller", "Product");
            product.SetAttribute("action", "Category");
            product.SetAttribute("id", "41");

            home.AppendChild(product);
            root.AppendChild(home);





            doc.Save(System.IO.Path.Combine(Server.MapPath("~"), "Mvc.sitemap"));
          
        }

    }//end class
}