using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DAL;
using Web.Models.IdentityEntities;

namespace Web.BLL.Identity
{
    public static class MemberShipTools
    {
        private static MyContext db;

        public static UserStore<User> NewUserStore() => new UserStore<User>(db ?? new MyContext());
        public static UserManager<User> NewUserManager() => new UserManager<User>(NewUserStore());


        public static RoleStore<Role> NewRoleStore() => new RoleStore<Role>(db ?? new MyContext());
        public static RoleManager<Role> NewRoleManager() => new RoleManager<Role>(NewRoleStore());
    }
}
