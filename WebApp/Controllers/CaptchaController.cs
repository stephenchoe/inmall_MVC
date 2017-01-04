using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using BLL;

namespace WebApp.Controllers
{
    public class CaptchaController : Controller
    {
        private readonly ICaptchaService captchaService;
        public CaptchaController(ICaptchaService captchaService)
        {
            this.captchaService = captchaService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<bool> CaptchaValidAsync()
        {
            string recaptchaVarName = ConfigurationManager.AppSettings["Recaptcha2VarName"];
            string recaptchaResponse = Request.Form[recaptchaVarName];
            string gRecaptchaRemoteIp = Request.UserHostAddress;

            bool validSuccess = await captchaService.SimpleVerifyAsync(recaptchaResponse, gRecaptchaRemoteIp);
            return validSuccess;
        }
    }
}