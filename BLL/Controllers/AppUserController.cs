using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using Model;


namespace BLL
{
    public class AppUserController : AppControllerBase
    {
        private AppUserManager _userManager;
        private AppRoleManager _roleManager;
        private AppSignInManager _signInManager;

        private readonly IEmailService _emailService;
        private readonly IAddressService _addressService;

        public AppUserController(IErrorLogService errorLogService, IEmailService emailService, IAddressService addressService)
            : base(errorLogService)
        {
            this._emailService = emailService;
            this._addressService = addressService;
        }
        #region    Properties
        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public AppRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<AppRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }

        }
        public AppSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AppSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        // 新增外部登入時用來當做 XSRF 保護
        private const string xsrfKey = "XsrfId";
        protected string XsrfKey
        {
            get { return xsrfKey; }
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected IEmailService EmailService
        {
            get { return _emailService; }
        }
        protected IAddressService AddressService
        {
            get { return _addressService; }
        }
        #endregion


        protected AppUser FindUserByEmail(string email,bool throwException=false)
        {
            var user = UserManager.FindByEmail(email);
            if (user != null) return user;

            if (throwException)
            {
                throw new Exception("");
            }
            else
            {
                return null;
            }
        }

        #region   Address

        [HttpGet]
        public ActionResult GetDistrictOptions(int cid)
        {
            return PartialView("_Options", DistrictOptions(cid));
        }

        protected IEnumerable<SelectListItem> CityOptions(int cityId = 0)
        {
            var cities = AddressService.GetAllCities();

            return cities.ToSelectListItems(cityId);
        }
        protected IEnumerable<SelectListItem> DistrictOptions(int cityId, int districtId = 0)
        {
            var districts = AddressService.GetDistricts(cityId);

            return districts.ToSelectListItems(districtId);
        }
        protected string GetZipCode(int districtId)
        {
            return AddressService.GetZipCode(districtId);
        }

        #endregion


       
    }
}
