using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Models
{
    public abstract class BaseForm
    {
        public string FormId { get; set; }
        public bool Ajax { get; set; }
        //public string BeginCallBack { get; set; }
        //public string CompleteCallBack { get; set; }
        //public string SuccessCallBack { get; set; }
        //public string FailedCallBack { get; set; }
        //public string UpdateTargetId { get; set; }
        //public string Area { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public object RouteValues { get; set; }

      
        public bool Confirmed { get; set; }
        public string ErrorMsg { get; set; }
        public string RedirectUrl { get; set; }


        protected static string ErrorIconHtml
        {
            get { return "<span class='glyphicon glyphicon-exclamation-sign'></span>"; }

        }

        public string CurrentUserId { get; set; }

        public StatusAlert StatusAlert { get; set; }



    }

    public abstract class BaseRecordForm : BaseForm
    {
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始日期")]

        [Required(ErrorMessage = "<label class='text-danger' ><span class='glyphicon glyphicon-exclamation-sign'></span> 請填寫開始日期</label>")]
        public DateTime? BeginDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束日期")]
        public virtual DateTime? EndDate { get; set; }

        [Display(Name = "備註")]
        public string PS { get; set; }



    }
    public class BaseEntityView
    {
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "建檔日期")]
        public DateTime? CreatedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "建檔人員")]
        public string CreatorName { get; set; }
        public string CreatedBy { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "最後更新")]
        public DateTime? LastUpdated { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "更新人員")]
        public string UpdaterName { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class BaseRecordView : BaseEntityView
    {
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始日期")]
        public DateTime? BeginDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束日期")]
        public DateTime? EndDate { get; set; }


        [Display(Name = "備註")]
        public string PS { get; set; }

    }
    public abstract class BaseSearchForm : BaseForm
    {
        private string pageReplacement = "_page_";
        public string SearchUrl { get; set; }
        public string UrlStringParameters { get; set; }

        public string KeyWord { get; set; }
        public string SortBy { get; set; }
        public int OrderOptionId { get; set; }
        public bool OrderByDescending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public bool ShowCreateButton { get; set; }

        public string PageReplacement
        {
            get { return pageReplacement; }
            set { pageReplacement = value; }
        }

        public static string GetPageReplacement()
        {
            return "_page_";
        }

    }
}
