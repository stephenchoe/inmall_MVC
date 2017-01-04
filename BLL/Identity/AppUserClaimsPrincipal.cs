using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Security;

namespace BLL
{
    public class AppUserClaimsPrincipal : ClaimsPrincipal
    {
        public AppUserClaimsPrincipal(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public string UserId
        {
            get
            {
                return this.FindFirst("UserId").Value;
            }
        }

        public string NickName
        {
            get
            {
                return this.FindFirst("NickName").Value;
            }
        }

        public string RememberMe
        {
            get
            {
                return this.FindFirst("RememberMe").Value;
            }
        }
    }
}
