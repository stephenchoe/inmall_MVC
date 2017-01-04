using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;

using Web.Core.Controllers;
using Web.Core.ErrorHandler;

using Model;
using System.Security.Claims;
using Web.Core.Models;

namespace BLL
{
    public class AppControllerBase : BaseController
    {
        private readonly IErrorLogService errorLogService;

        public AppControllerBase(IErrorLogService errorLogService)
        {
            this.errorLogService = errorLogService;
            InitController();
            
        }



        protected string SiteName { get { return ConfigurationManager.AppSettings["siteName"]; } }
        protected string PageTitle
        {
            get { return ViewBag.PageTitle;}
            set { ViewBag.PageTitle = value; }
        }
        protected string Recaptcha2VarName { get { return ConfigurationManager.AppSettings["Recaptcha2VarName"]; } }

        public AppUserClaimsPrincipal CurrentUser
        {
            get
            {
                return new AppUserClaimsPrincipal(this.User as ClaimsPrincipal);
            }
        }


        protected override void InitController()
        {
            DefaultErrorMsg = "很抱歉：系統出現錯誤，請稍後再試。";
        }

        protected override void HandleException(Exception ex)
        {
            errorLogService.LogException(ex);
        }

        protected void SetStatusAlert(StatusAlert model, StatusAlertType type, string title, string msg, bool showConfirm)
        {
                model.Type = type;
                model.Title = title;
                model.StatusText = msg;              
                model.ShowConfirmButton = showConfirm;
        }
        protected  ActionResult RedirectToLocalPage(string returnUrl="")
        {
            if (String.IsNullOrEmpty(returnUrl)) { return Redirect(GetBaseUrl()); }

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return Redirect(GetBaseUrl()); 
        }
        protected string GetBaseUrl()
        {

            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, appUrl);

            return baseUrl;
        }
       
    }
}
