using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebShop.Models.Db;

namespace WebShop.Models.Security
{
    public class CustomRoleProvider : RoleProvider
    {
        dbWebShop db = new dbWebShop();
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string roles = db.USERs.Single(a => a.USERID.Equals(username)).ROLES;
            if (roles != null)
                return new string[] { roles };
            else
                return new string[] { }; 
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = db.USERs.Single(a => a.USERID == username);
            var roles = user.ROLES;
            if (user != null)
                return roles.Any(r => r.Equals(roleName));
            else
                return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}