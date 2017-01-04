using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList.Mvc;

using Core.Extensions;
using Web.Core.Models;

namespace BLL
{
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        protected AppUserClaimsPrincipal CurrentUser
        {
            get
            {
                return new AppUserClaimsPrincipal(this.User as ClaimsPrincipal);
            }
        }
        protected string PageTitle
        {
            get { return ViewBag.PageTitle; }
            set { ViewBag.PageTitle = value; }
        }
        protected string SiteTitle
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["siteTitle"]; }
        }
        protected string SiteName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["siteName"]; }
        }
      
       
        protected string RemoveHtmlTags(string htmlString)
        {

            return htmlString.RemoveHtmlTags();

        }
        protected PagedListRenderOptions GetPagedListRenderOption()
        {
            return new PagedListRenderOptions()
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                LinkToFirstPageFormat = "第一頁",
                LinkToPreviousPageFormat = "<span class='glyphicon glyphicon-chevron-left' aria-hidden='true'></span> 上一頁",
                LinkToNextPageFormat = "下一頁 <span class='glyphicon glyphicon-chevron-right' aria-hidden='true'></span>",
                LinkToLastPageFormat = "最末頁",

            };
        }
        protected string CompleteCallBack(string callBack)
        {
            if (callBack != "" && !callBack.EndsWith(";"))
            {
                callBack += "();";
            }
            return callBack;

        }

        protected string GetStatusTypeText(StatusAlert model)
        {
            if (model.Type ==StatusAlertType.Success) return "success";
            if (model.Type == StatusAlertType.Success) return "error";
            return "warning";
  
        }
        protected string GetStatusSuccessMsg(StatusAlert model)
        {
            if (model.Type == StatusAlertType.Success) return "success";
            if (model.Type == StatusAlertType.Success) return "error";
            return "warning";

        }


    }
    public abstract class BaseViewPage : WebViewPage<dynamic>
    {


    }
}
