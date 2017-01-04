using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Recaptcha;

namespace BLL
{
    public interface ICaptchaService
    {
        Task<bool> SimpleVerifyAsync(string gRecaptchaResponse, string gRecaptchaRemoteIp);
        Task<string[]> VerifyAsync(string gRecaptchaResponse, string gRecaptchaRemoteIp);
    }
    public class CaptchaService : ICaptchaService
    {
        private readonly IRecaptcha2Verifier recaptcha2Verifier;
        public CaptchaService(IRecaptcha2Verifier recaptcha2Verifier)
        {
            this.recaptcha2Verifier = recaptcha2Verifier;
        }
        public async Task<bool> SimpleVerifyAsync(string gRecaptchaResponse, string gRecaptchaRemoteIp)
        {
            bool success = false;
            var result = await recaptcha2Verifier.VerifyAsync(gRecaptchaResponse, gRecaptchaRemoteIp);
            if (result.Length == 0) success = true;

            return success;
        }
        public async Task<string[]> VerifyAsync(string gRecaptchaResponse, string gRecaptchaRemoteIp)
        {
            var result = await recaptcha2Verifier.VerifyAsync(gRecaptchaResponse, gRecaptchaResponse);
            return result;
        }
    }
}
