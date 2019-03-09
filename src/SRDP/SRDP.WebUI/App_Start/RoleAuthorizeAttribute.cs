using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.App_Start
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public RoleAuthorizeAttribute(params object[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("Los roles solo pueden ser del tipo enum", "roles");

            var rolesList = roles.Select(r => Enum.GetName(r.GetType(), r)).ToList();
            Roles = string.Join(",", rolesList);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var url = new UrlHelper(filterContext.RequestContext);
            var accessDeniedUrl = url.Action("AccessDenied", "Error");

            if (!String.IsNullOrEmpty(base.Roles))
            {
                var isRoleError = true;
                var rolesAllowed = base.Roles.Split(',');
                var user = filterContext.HttpContext.User;
                if (user != null && rolesAllowed.Any())
                {
                    foreach (var role in rolesAllowed)
                    {
                        if (user.IsInRole(role))
                            isRoleError = false;
                    }
                }

                if (isRoleError)
                {
                    if (request.IsAjaxRequest())
                        filterContext.Result = new JsonResult { Data = new { error = true, signinerror = "true", message = "Acceso Denegado" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    else
                        filterContext.Result = new RedirectResult(accessDeniedUrl);
                }
            }
        }
    }
}