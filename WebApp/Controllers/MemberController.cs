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

using WebApp.Models;
using BLL;
using Model;
using System.Configuration;
using Web.Core.Models;


namespace WebApp.Controllers
{
    public class MemberController : AppUserController
    {
       
        private readonly ICaptchaService captchaService;

        public MemberController(IErrorLogService errorLogService,IEmailService emailService, IAddressService addressService,  ICaptchaService captchaService)
            : base(errorLogService, emailService, addressService)
        {
          
            this.captchaService = captchaService;
            
        }


        #region    Properties
        
       
        #endregion

        


        // GET: Member
        [HttpGet]
        public ActionResult Terms()
        {
            return View();
        }

        #region   Register
        [HttpGet]
        public ActionResult Register()
        {
            var defaultDate = new DateTime(1980, 6, 1);
            var model = new RegisterViewModel() { DOB = defaultDate, Gender = true };

            return RegisterView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            StatusAlertType alertType = StatusAlertType.Error;
            string errorTitle = "註冊失敗";
            string errorMsg = "系統暫時無回應，請稍後再試。";
            bool showConfirm = true;

            var resultStatusModel = new RegisterResultModel();
            var resultRegister = RegisterResult.Failure;


            try
            {
                if (!ModelState.IsValid)
                {
                    return RegisterView(model);
                }

                var user = new AppUser
                {
                    UserName = model.Email.Trim(),
                    Email = model.Email.Trim(),
                    DOB = Convert.ToDateTime(model.DOB),
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber.Trim(),
                    Address = new Address
                    {
                        CityId = model.CityId,
                        DistrictId = model.DistrictId,
                        StreetAddress = model.StreetAddress.Trim(),
                        ZipCode = GetZipCode(model.DistrictId)
                    },

                    CreateDate = DateTime.Now.ConvertToTaipeiTime()
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)  //註冊失敗
                {
                    AddErrors(result);

                    alertType = StatusAlertType.Error;
                    resultRegister = RegisterResult.Failure;
                    errorMsg = "系統暫時無回應，請稍後再試。";

                    model.ResultStatus = GetRegisterResultModel(alertType, errorTitle, errorMsg, showConfirm, resultRegister); ;
                    return RegisterView(model);
                }

                //加入角色
                try
                {
                    AddUserToMemberRole(user.Id);
                }
                catch (Exception addRoleException)
                {
                    HandleException(addRoleException);
                    //加入角色失敗,將剛加入的會員資料刪除

                    UserManager.Delete(user);

                    alertType = StatusAlertType.Error;
                    resultRegister = RegisterResult.AddRoleError;
                    errorMsg = "系統暫時無回應，請稍後再試。";

                    model.ResultStatus = GetRegisterResultModel(alertType, errorTitle, errorMsg, showConfirm, resultRegister); ;
                    return RegisterView(model);

                }



                //加入會員成功,mail會員註冊認證信
                await SendRegisterConfirmEmail(user);

                RegisterConfirmModel registerConfirmModel = new RegisterConfirmModel
                {
                    Email = user.Email,
                    NickName = String.Format("{0} {1}", user.FullName, user.GenderText)
                };

                return View("RegisterInstruction", registerConfirmModel);

            }
            catch (Exception ex)
            {
                HandleException(ex);

                alertType = StatusAlertType.Error;
                resultRegister = RegisterResult.Exception;
                errorMsg = "系統暫時無回應，請稍後再試。";

                model.ResultStatus = GetRegisterResultModel(alertType, errorTitle, errorMsg, showConfirm, resultRegister);
                return RegisterView(model);


            }

        }

        [HttpPost]
        public bool CheckUserName()
        {
            string email = Request.Form["email"];
            var user = UserManager.FindByEmail(email);
            return (user == null);
        }

        [ChildActionOnly]
        ViewResult RegisterView(RegisterViewModel model)
        {
            LoadOptions(model);
            return View("Register", model);
        }

        void AddUserToMemberRole(string userId)
        {
            //加入角色
            string roleMemberName = RoleManager.GetRoleNameByEnum(AppRoles.Member);

            var result = UserManager.AddToRole(userId, roleMemberName);
            if (!result.Succeeded) throw new Exception("將新會員加入角色時發生錯誤.");
        }
        RegisterResultModel GetRegisterResultModel(StatusAlertType type, string title, string msg, bool showConfirm, RegisterResult registerResult)
        {
            var model = new RegisterResultModel() { RegisterResult = registerResult };
            SetStatusAlert(model, type, title, msg, showConfirm);

            return model;
        }
        void LoadOptions(RegisterViewModel model)
        {
            model.CityOptions = CityOptions(model.CityId);

            if (model.CityId < 1)
            {
                var first = model.CityOptions.FirstOrDefault();
                model.CityId = Convert.ToInt32(first.Value);
            }

            model.DistrictOptions = DistrictOptions(model.CityId, model.DistrictId);
        }


        #endregion


        #region   RegisterConfirm
        public async Task<ActionResult> RegisterConfirm(string user, string code)
        {
            try
            {
                string userId = user;
                if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(code))
                {
                    return HttpNotFound();
                }

                AppUser newUser = await UserManager.FindByIdAsync(userId);
                if (newUser == null)
                {
                    return HttpNotFound();
                }

                if (!newUser.EmailConfirmed)
                {
                    var result = await UserManager.ConfirmEmailAsync(userId, code);
                    if (!result.Succeeded)
                    {
                        //無效的驗證碼
                        var modelConfirmFailed = new RegisterConfirmModel
                        {
                            ConfirmOK = false,
                            UserId = userId
                        };

                        return View(modelConfirmFailed);
                    }
                }


                //驗證成功
                var modelConfirmSuccess = new RegisterConfirmModel
                {
                    ConfirmOK = true,
                    UserId = userId,
                    NickName = userId
                };

                return View(modelConfirmSuccess);


            }
            catch (Exception ex)
            {
                HandleException(ex);
                return ErrorPage();
            }


        }

        public ActionResult NeedRegisterConfirm(string userName)
        {
            try
            {
                if (String.IsNullOrEmpty(userName)) return HttpNotFound();

                AppUser user = UserManager.FindByName(userName);
                if (user == null) return HttpNotFound();

                if (user.EmailConfirmed) return HttpNotFound();  //已經驗證過

                return NeedRegisterConfirm(user);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return ErrorPage();
            }
        }

      
        ActionResult NeedRegisterConfirm(AppUser user, bool confirmOK = false)
        {
            var model = new RegisterConfirmModel
            {
                ConfirmOK = confirmOK,
                Email = user.Email,
                NickName = user.NickName,
                UserId = user.Id
            };

            return View("NeedRegisterConfirm", model);
        }

        //使用者申請重發驗證信
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendRegisterConfirmMail(RegisterConfirmModel form)
        {
            try
            {
                AppUser user = UserManager.FindById(form.UserId);
                if (user == null)
                {
                    return ErrorPage();
                }

                //發送email驗證信
                await SendRegisterConfirmEmail(user);

                //成功
                form.NickName = user.NickName;
                form.Email = user.Email;
                return View("RegisterInstruction", form);

            }
            catch (Exception ex)
            {

                HandleException(ex);

                return ErrorPage();
            }
        }

        async Task SendRegisterConfirmEmail(AppUser user)
        {
           
            string title = String.Format("{0} 會員註冊Email認證信", SiteName);
            string subject = title;
            string nickName = user.NickName;

            string emailTemplate = System.IO.File.ReadAllText(Server.MapPath("~/Views/Shared/EmailTemplates/RegisterMail.html"));

            string userId = user.Id;
            string code = UserManager.GenerateEmailConfirmationToken(userId);
            var validateUrl = Url.Action("RegisterConfirm", "Member", new { user = userId, code = code }, protocol: Request.Url.Scheme);

            string mailBody = emailTemplate.Replace("{{TitleText}}", title).Replace("{{UserName}}", nickName)
                                                        .Replace("{{ValidateUrl}}", validateUrl);

            string to = user.Email;
            string from = ConfigurationManager.AppSettings["emailAddress"];
            string senderName = SiteName;

            await EmailService.SendAsync(title, to, mailBody, from, senderName);
        }


        #endregion


        #region  Login

        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {
            if (Request.IsAuthenticated) return RedirectToLocalPage(returnUrl);

            var model = new LoginViewModel
            {
                ReturnUrl = GetReturnUrl(returnUrl)
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            StatusAlertType alertType = StatusAlertType.Error;
            string errorTitle = "登入失敗";
            string errorMsg = "系統暫時無回應，請稍後再試。";
            bool showConfirm = true;
            string successTitle = "登入成功";
            string successMsg = "";

            var loginResult = LoginResult.Failure;
            try
            {
            
                string userName = model.Email.Trim();
                string password = model.Password.Trim();

                var result = await SignInManager.PasswordSignInAsync(userName, password, shouldLockout: true);
                switch (result)
                {
                    case SignInStatus.Success:   //登入成功
                        alertType = StatusAlertType.Success;
                        loginResult = LoginResult.Success;
                        var user = UserManager.FindByName(userName);
                        successMsg = String.Format("登入成功,{0}歡迎您回來", user.FullName);

                        model.ResultStatus = GetLoginResultModel(alertType, successTitle, successMsg, showConfirm, loginResult);
                        return LoginPartial(model);

                    case SignInStatus.RequiresVerification:
                        alertType = StatusAlertType.Error;
                        loginResult = LoginResult.RequiresVerification;
                        errorMsg = "您的會員帳號尚未完成Email認證。";

                        model.ResultStatus = GetLoginResultModel(alertType, errorTitle, errorMsg, showConfirm, loginResult);
                        return LoginPartial(model);


                    case SignInStatus.LockedOut:  //Lockout
                        alertType = StatusAlertType.Error;
                        loginResult = LoginResult.Locked;
                        errorMsg = "您的會員帳號密碼已被鎖定。";

                        model.ResultStatus = GetLoginResultModel(alertType, errorTitle, errorMsg, showConfirm, loginResult);
                        return LoginPartial(model);


                    case SignInStatus.Failure:
                    default:  //登入失敗
                        alertType = StatusAlertType.Error;
                        loginResult = LoginResult.Failure;
                        errorMsg = "請確認您輸入的電子信箱和密碼是否正確。";

                        model.ResultStatus = GetLoginResultModel(alertType, errorTitle, errorMsg, showConfirm, loginResult);
                        return LoginPartial(model);
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);

                alertType = StatusAlertType.Error;
                loginResult = LoginResult.Exception;
                errorMsg = "系統暫時無回應，請稍後再試。";

                model.ResultStatus = GetLoginResultModel(alertType, errorTitle, errorMsg, showConfirm, loginResult);
                return LoginPartial(model);
            }

        }
        [ChildActionOnly]
        PartialViewResult LoginPartial(LoginViewModel model)
        {
            string viewName = "Login";
            string returnUrl = GetReturnUrl(model.ReturnUrl);
            model.ReturnUrl = returnUrl;
            return PartialView(viewName, model);
        }
        string GetReturnUrl(string returnUrl)
        {
            if (String.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }
            return returnUrl;
        }
        LoginResultModel GetLoginResultModel(StatusAlertType type, string title, string msg, bool showConfirm, LoginResult loginResult)
        {
            var model = new LoginResultModel() { LoginResult = loginResult };
            SetStatusAlert(model, type, title, msg, showConfirm);

            return model;
        }

        #endregion


        #region   LogOut

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
           
            try
            {
                AuthenticationManager.SignOut();

                return RedirectToLocalPage();
              
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return ErrorPage();
            }
        }

        
        #endregion
       

        #region   ForgotPassword
        [HttpGet]
        public ActionResult ForgotPassword()           
        {
            string test = GetBaseUrl();
            return View(new ForgotPasswordViewModel());
        }
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                string userName = model.Email.Trim();
                var user = await UserManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("Email", "這個Email沒有註冊過");
                    return View(model);
                }

                if (!user.EmailConfirmed)
                {
                    return NeedRegisterConfirm(user);
                }

                await SendResetPasswordEmail(user);

                return ForgotPasswordInstruction(user.Email, user.FirstName);

            }
            catch (Exception ex)
            {
                HandleException(ex);

                return ErrorPage();

            }


        }
     
        ActionResult ForgotPasswordInstruction(string email, string nickName)
        {
            var model = new ForgotPasswordInstruction()
            {
                Email = email,
                NickName = nickName
            };
            return View("ForgotPasswordInstruction", model);
        }

        private async Task SendResetPasswordEmail(AppUser user)
        {
            string mailHtmlTemplate = System.IO.File.ReadAllText(Server.MapPath("~/Views/Shared/EmailTemplates/PasswordMail.html"));
            string subject = String.Format("{0} 忘記密碼回函", SiteName);
            string title = String.Format("{0} 忘記密碼小幫手", SiteName);
            string nickName = user.FirstName;

            string userId = user.Id;
            string code =await UserManager.GeneratePasswordResetTokenAsync(userId);
            var validateUrl = Url.Action("ResetPassword", "Member", new { user = userId, code = code }, protocol: Request.Url.Scheme);

            string mailBody = mailHtmlTemplate.Replace("{{TitleText}}", title).Replace("{{UserName}}", nickName)
                                                        .Replace("{{ValidateUrl}}", validateUrl);

            string to = user.Email;
            string from = ConfigurationManager.AppSettings["emailAddress"];
            string senderName = SiteName;

            await EmailService.SendAsync(subject, to, mailBody, from, senderName);


        }
        ForgotPasswordResultModel GetForgotPasswordResultModel(StatusAlertType type, string title, string msg, bool showConfirm, ForgotPasswordResult result)
        {
            var model = new ForgotPasswordResultModel() { ForgotPasswordResult = result };
            SetStatusAlert(model, type, title, msg, showConfirm);

            return model;
        }

        #endregion

        #region   ResetPassword

        [HttpGet]
        public async Task<ActionResult> ResetPassword(string user, string code)
        {
            try
            {
                string userId = user;
                if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(code))
                {
                    return HttpNotFound();
                }

                AppUser theUser = await UserManager.FindByIdAsync(userId);
                if (theUser == null)
                {
                    return HttpNotFound();
                }

                var model = new ResetPasswordModel()
                {
                    TokenCode = code
                };

                return View(model);

            }
            catch (Exception ex)
            {
                HandleException(ex);
                return ErrorPage();
            }


         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            StatusAlertType alertType = StatusAlertType.Error;
            string errorTitle = "重設密碼失敗";
            string errorMsg = "";
            bool showConfirm = true;
            string successTitle = "重設密碼成功";
            string successMsg = "";

            var resetPasswordResult = ResetPasswordResult.Failure;
          
            try
            {
                if (!ModelState.IsValid) return PartialView(model);   
            
                string email=model.Email.Trim();
                string code = model.TokenCode;
                string password = model.Password.Trim();                

                var user = await UserManager.FindByNameAsync(email);
                if (user != null)
                {
                    string userId = user.Id;
                    var result = await UserManager.ResetPasswordAsync(user.Id, model.TokenCode, model.Password);
                    if (result.Succeeded)
                    {
                        alertType = StatusAlertType.Success;
                        resetPasswordResult = ResetPasswordResult.Success;

                        model.ResultStatus = GetResetPasswordResultModel(alertType, successTitle, successMsg, showConfirm, resetPasswordResult);
                        return PartialView(model);
                    }
                }

                alertType = StatusAlertType.Error;
                resetPasswordResult = ResetPasswordResult.Failure;

                model.ResultStatus = GetResetPasswordResultModel(alertType, errorTitle, errorMsg, showConfirm, resetPasswordResult);
                return PartialView(model);

            }
            catch (Exception ex)
            {
                HandleException(ex);

                alertType = StatusAlertType.Error;
                resetPasswordResult = ResetPasswordResult.Exception;
                errorMsg = "系統暫時無回應，請稍後再試。";

                model.ResultStatus = GetResetPasswordResultModel(alertType, errorTitle, errorMsg, showConfirm, resetPasswordResult);
                return PartialView(model);
            }
          
        }
        ResetPasswordResultModel GetResetPasswordResultModel(StatusAlertType type, string title, string msg, bool showConfirm, ResetPasswordResult result)
        {
            var model = new ResetPasswordResultModel() { ResetPasswordResult = result };
            SetStatusAlert(model, type, title, msg, showConfirm);

            return model;
        }
        #endregion

        public async Task<ActionResult> Lockout(string userName)
        {
            if (String.IsNullOrEmpty(userName)) return HttpNotFound();
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null) return HttpNotFound();
            return View("Lockout");
        }

       


        #region   SeedData
        public ActionResult SeedData()
        {
            var city = AddressService.GetCityByName("花蓮縣");
            var district = AddressService.GetDistrictByName("吉安鄉");

            SeedDataInitializer.CreateBoss(UserManager, city.Id, district.Id);

            //var all = this.cityService.GetAll();
            //ViewBag.test = all.FirstOrDefault().Name;

            return View("Login");
        }

        public ActionResult DropAlways()
        {
            // Database.SetInitializer<InmallContext>(new DropCreateAlwaysInitializer<InmallContext>(UserManager,RoleManager));


            return View("Login");
        }
        public ActionResult DropIfChanges()
        {
            return View("Login");
        }

        #endregion


        void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult PasswordConfirm()
        {
            return View();
        }




    }

}