using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Web.Core.Models;

namespace WebApp.Models
{
    public class ProfilesViewModel
    {
        [Required(ErrorMessage = "此欄位為必填")]
        [EmailAddress(ErrorMessage = "請輸入有效的電子郵件地址")]
        [Display(Name = "電子郵件：")]
        public string Email { get; set; }

        [Required(ErrorMessage = "此欄位為必填")]
        [Display(Name = "姓")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "此欄位為必填")]
        [Display(Name = "名")]
        public string FirstName { get; set; }


        public bool Gender { get; set; }

    
        [Required(ErrorMessage = "此欄位為必填")]
        [Display(Name = "出生日期：")]
        [DisplayFormat(DataFormatString = "{0:yyyy/M/d}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }


        [Required(ErrorMessage = "此欄位為必填")]
        [Display(Name = "手機號碼：")]
        public string PhoneNumber { get; set; }


        public int CityId { get; set; }
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "此欄位為必填")]
        public string StreetAddress { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> CityOptions { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> DistrictOptions { get; set; }

        

    }

    public class ProfilesUpdateModel : ProfilesViewModel
    {
        public ProfilesUpdateResultModel ResultStatus { get; set; }
    }
    public class ProfilesUpdateResultModel : StatusAlert
    {
        public ProfilesUpdateResult ProfilesUpdateResult { get; set; }
    }

    public enum ProfilesUpdateResult
    {
        Success,
        Failure,      
        Exception

    }

   
}