using SRDP.Application.UseCases.GetRolesUsuario;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace SRDP.WebUI.App_Start
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly IGetRolesUsuarioUserCase _getRolesUsuarioUserCase;

        public CustomRoleProvider(IGetRolesUsuarioUserCase getRolesUsuarioUserCase)
        {
            _getRolesUsuarioUserCase = getRolesUsuarioUserCase;
        }

        public CustomRoleProvider()
        {
            _getRolesUsuarioUserCase = new GetRolesUsuarioUserCase(new Persitence.DapperDataAccess.Repositories.RolesUsuarioRepository(ConfigurationManager.ConnectionStrings["SRDPConnection"].ConnectionString));
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var roles = _getRolesUsuarioUserCase.ExecuteSync(username);
            return roles.Count != 0 && roles.Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            var roles = _getRolesUsuarioUserCase.ExecuteSync(username);
            return roles.ToArray();
        }

        #region Not Implemented

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}