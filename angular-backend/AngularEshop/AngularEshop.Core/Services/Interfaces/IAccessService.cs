using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface IAccessService : IDisposable
    {
        #region user role
        Task<bool> CheckUserRole(long userId, string role);
        #endregion
    }
}
