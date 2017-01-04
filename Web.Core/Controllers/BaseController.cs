using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using System.Security.Claims;
using System.Text.RegularExpressions;

using Core.Extensions;

namespace Web.Core.Controllers
{
    public abstract  class BaseController : Controller
    {
        protected string ThisControllerName { get; set; }
        protected string DefaultErrorMsg{ get; set; }

        
        protected virtual void InitController()
        {
            DefaultErrorMsg = "載入資料失敗,請稍後再試.如果問題持續發生,請聯絡系統管理員.";
        }
        

        protected string SessionID
        {
            get
            {
                return Session["sessionid"] != null ? Session["sessionid"].ToString() : "";
            }
            set { Session["sessionid"] = value; }
        }

       
        protected string AreaName { get; set; }


        protected virtual void HandleException(Exception ex)
        {
            //if (ex is ValidationException)
            //{
            //    ModelState.ImportValidationErrors(ex as ValidationException);
            //    TempData[SiteKeys.TempData.Error] = Errors.Messages.ValidationErrors;
            //    return;
            //}
            //if (ex is DataNotFoundException)
            //{
            //    //TempData["Error"] = ex.Message;
            //    //return;
            //}


            // any exceptions 'below' this one are logged
            //this.errorLogger.LogException(ex);


            //if (ex is PersistenceException)
            //{
            //    TempData["Error"] = string.Format("Unable to Save Changes.");
            //    return;
            //}

            // unhandled error
            //TempData["Error"] = "Oops. Unexpected error.";
        }

        //上線前取消註解
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    Exception ex = filterContext.Exception;
        //    HandleException(ex);

        //    filterContext.ExceptionHandled = true;
        //    filterContext.Result = new ViewResult()
        //    {
        //        ViewName = "Error"
        //    };

        //}
        protected bool IsAjax
        {
            get { return Request.IsAjaxRequest(); }
        }
        public static string GetUrl(string action, string controller, string area = "")
        {
            if (String.IsNullOrEmpty(area))
            {
                return String.Format("/{0}/{1}", controller, action);
            }

            return String.Format("/{0}/{1}/{2}", area, controller, action);

        }

        protected JsonResult SuccessJsonResult(string msg)
        {
            return Json(new { Message = msg, Code = "1" }, JsonRequestBehavior.AllowGet);
        }
        protected JsonResult FailedJsonResult(string msg)
        {
            return Json(new { Message = msg, Code = "0" }, JsonRequestBehavior.AllowGet);
        }
        protected JsonResult ThrowJsonError(string msg="", string code = "")
        {
            if (String.IsNullOrEmpty(msg)) msg = DefaultErrorMsg;
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            Response.StatusDescription = msg;
            return Json(new { Message = msg, Code = code }, JsonRequestBehavior.AllowGet);
        }
        protected string WarningConfirmViewName
        {
            get { return "_WarningConfirm"; }
        }
        protected string EditorTemplateFolderPath
        {
            get { return "~/Views/Shared/EditorTemplates/"; }
        }

        protected string DisplayTemplateFolderPath
        {
            get { return "~/Views/Shared/DisplayTemplates/"; }
        }
        protected string EmailTemplateFolderPath
        {
            get { return "~/Views/Shared/EmailTemplates/"; }
        }

        protected virtual string GetErrorMsg(string msg)
        {
            return String.Format("<label class='text-danger' ><span class='glyphicon glyphicon-exclamation-sign'></span>{0}</label>", msg);
        }
       
       
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        protected string RemoveHtmlTags(string content)
        {
            return Regex.Replace(content, @"<[^>]*>", String.Empty);
        }

        public virtual ActionResult ErrorPage(string msg = "")
        {
            if (String.IsNullOrEmpty(msg))
            {
                msg = this.DefaultErrorMsg;
            }
            ViewBag.Msg = msg;
            return View("Error");
        }

        protected int CheckPage<T>(int page, IEnumerable<T> enumerable, int pageSize)
        {
            int maxPage = enumerable.MaxPage(pageSize);
            if (page > maxPage) page = maxPage;
            if (page == 0) page = 1;
            return page;
        }

        protected ActionResult EmptyPartial()
        {
            return PartialView("_Empty");
        }

      

    }
}
