using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Core.Models;

namespace WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "此欄位為必填")]
        [Display(Name = "電子信箱：")]
        [EmailAddress(ErrorMessage = "電子郵件格式錯誤")]
        public string Email { get; set; }

        [Required(ErrorMessage = "此欄位為必填")]
        [DataType(DataType.Password)]
        [Display(Name = "登入密碼：")]
        public string Password { get; set; }

        [Display(Name = "記住我?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
        public string UserId { get; set; }

        public LoginResultModel ResultStatus { get; set; }
    }

    public class LoginResultModel : StatusAlert
    {
        public LoginResult LoginResult { get; set; }
    }

    public enum LoginResult
    {
        Success,
        Failure,
        Locked,
        RequiresVerification,
        Exception

    }
}