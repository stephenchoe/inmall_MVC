using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Configuration;

using Model;
using DAL;

namespace Service
{
    public class AppRoleManager : RoleManager<AppRole>
    {
        public AppRoleManager(IRoleStore<AppRole, string> roleStore)
            : base(roleStore)
        {
        }


        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            return new AppRoleManager(new RoleStore<AppRole>(context.Get<InmallContext>()));
        }

        public IEnumerable<AppRole> GetAdminRoles()
        {
            return Roles.Where(r => r.IsAdmin);
        }
        public IEnumerable<AppRole> GetNonAdminRoles()
        {
            return Roles.Where(r => !r.IsAdmin);
        }
      
       


    }

}
