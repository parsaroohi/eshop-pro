using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Extensions.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using AngularEshop.Core.Utilities.Common;
using System.Threading.Tasks;

namespace AngularEshop.WebApi.Identity
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        #region constructor
        private string role;
        private IAccessService accessService;
        public PermissionCheckerAttribute(string role)
        {
            this.role = role;
        }
        #endregion

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            accessService =
                (IAccessService)context.HttpContext.RequestServices.GetService(typeof(IAccessService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.GetUserId();
                var result = accessService.CheckUserRole(userId, role).Result;
                if (!result)
                {
                    context.Result = JsonResponseStatus.NoAccess();
                }
            }
            else
            {
                context.Result = JsonResponseStatus.NoAccess();
            }
        }
    }
}
