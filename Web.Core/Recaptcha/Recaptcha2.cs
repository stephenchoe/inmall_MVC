using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Web.Core.Recaptcha
{
    public interface IRecaptcha2Verifier
    {
        Task<string[]> VerifyAsync(string gRecaptchaResponse, string gRecaptchaRemoteIp);
    }
    public class Recaptcha2Verifier:IRecaptcha2Verifier
    {
        private readonly string gRecaptchaUrl = @" https://www.google.com/recaptcha/api/siteverify";
      
        private readonly string gRecaptchaSecret;
        

        public Recaptcha2Verifier(string gRecaptchaSecret)
        {
            this.gRecaptchaSecret = gRecaptchaSecret;
        }
        public async Task<string[]> VerifyAsync(string gRecaptchaResponse, string gRecaptchaRemoteIp)
        {
            var verifyResponse = await Recaptcha2VerifyAsync(gRecaptchaResponse, gRecaptchaRemoteIp);
            if (verifyResponse.Success) return new string[0];  // => 成功

            return verifyResponse.ErrorCodes;
        }
        public async Task<Recaptcha2VerifyResponse> Recaptcha2VerifyAsync(string gRecaptchaResponse, string gRecaptchaRemoteIp)
        {
            var result = new Recaptcha2VerifyResponse();
            string responseString;

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"secret",gRecaptchaSecret},
                    {"response",gRecaptchaResponse},
                    {"remoteip",gRecaptchaRemoteIp}
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(gRecaptchaUrl, content);
                responseString = await response.Content.ReadAsStringAsync();
            }

            if (String.IsNullOrWhiteSpace(responseString)) return result;

            result = JsonConvert.DeserializeObject<Recaptcha2VerifyResponse>(responseString);
            return result;

        }
    }

    public class Recaptcha2VerifyResponse
    {
        [JsonProperty("success")]
        private bool? _success = null;

        public bool Success { get { return _success == true; } }


        [JsonProperty("error-codes")]
        private string[] _errorCodes = null;

        public string[] ErrorCodes { get { return _errorCodes ?? new string[0]; } }

    }

   

    public static class Recaptcha2HtmlHelperExtension
    {
        public static MvcHtmlString Recaptcha2(this HtmlHelper html, string siteKey) //, string name = "g-recaptcha-response"
        {
            var gRecaptchaScript = "<script src=\"https://www.google.com/recaptcha/api.js\" async defer></script>";
            var gRecaptchaWidget = "<div class=\"g-recaptcha\" data-callback='RecaptchaCallback' data-sitekey=\"" + siteKey + "\"></div>";
           
            return new MvcHtmlString(gRecaptchaScript + gRecaptchaWidget);
        }
    }

    

}
