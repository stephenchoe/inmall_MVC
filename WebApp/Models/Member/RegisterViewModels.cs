using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Web.Core.Models;

namespace WebApp.Models
{
    public class RegisterViewModel : ProfilesViewModel
    {
        

        [Required(ErrorMessage = "此欄位為必填")]
        [StringLength(12, ErrorMessage = "密碼長度限定為6 ~ 12個字元", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "登入密碼：")]
        public string Password { get; set; }

        [Required(ErrorMessage = "此欄位為必填")]
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼：")]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符")]
        public string ConfirmPassword { get; set; }

       
        public RegisterResultModel ResultStatus { get; set; }

    }
    public class RegisterResultModel : StatusAlert
    {
        public RegisterResult RegisterResult { get; set; }
    }

    public enum RegisterResult
    {
        Success,
        Failure,     
        AddRoleError,
        Exception

    }

    public class RegisterConfirmModel
    {
        public string NickName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool ConfirmOK { get; set; }

    }
}