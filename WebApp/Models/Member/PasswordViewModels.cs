using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Core.Models;

namespace WebApp.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "此欄位為必填")]
        [EmailAddress(ErrorMessage = "請輸入有效的電子郵件地址")]
        [Display(Name = "電子郵件：")]
        public string Email { get; set; }


        public ForgotPasswordResultModel ResultStatus { get; set; }
    }

    public class ForgotPasswordResultModel : StatusAlert
    {
        public ForgotPasswordResult ForgotPasswordResult { get; set; }
    }

    public enum ForgotPasswordResult
    {
        Success,
        Failure,
        RequiresVerification,
        Exception

    }

    public class ForgotPasswordInstruction
    {
        public string Email { get; set; }
        public string NickName { get; set; }
    }


    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "此欄位為必填")]       
        [DataType(DataType.Password)]
        [Display(Name = "舊密碼：")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "此欄位為必填")]
        [StringLength(12, ErrorMessage = "密碼長度限定為6 ~ 12個字元", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密碼：")]
        public string Password { get; set; }

        [Required(ErrorMessage = "此欄位為必填")]
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼：")]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符")]
        public string ConfirmPassword { get; set; }


        public ChangePasswordResultModel ResultStatus { get; set; }
    }

    public class ChangePasswordResultModel : StatusAlert
    {
        public ChangePasswordResult ChangePasswordResult { get; set; }
    }

    public enum ChangePasswordResult
    {
        Success,
        Failure,       
        Exception

    }


    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "此欄位為必填")]
        [EmailAddress(ErrorMessage = "請輸入有效的電子郵件地址")]
        [Display(Name = "電子郵件：")]
        public string Email { get; set; }
        

        [Required(ErrorMessage = "此欄位為必填")]
        [StringLength(12, ErrorMessage = "密碼長度限定為6 ~ 12個字元", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "設定密碼：")]
        public string Password { get; set; }

        [Required(ErrorMessage = "此欄位為必填")]
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼：")]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符")]
        public string ConfirmPassword { get; set; }

        public string TokenCode { get; set; }


        public ResetPasswordResultModel ResultStatus { get; set; }

    }

    public class ResetPasswordResultModel : StatusAlert
    {
        public ResetPasswordResult ResetPasswordResult { get; set; }
    }

    public enum ResetPasswordResult
    {
        Success,
        Failure,
        Exception

    }


}