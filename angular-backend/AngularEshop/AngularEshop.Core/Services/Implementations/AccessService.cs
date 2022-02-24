using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.DataLayer.Entities.Access;
using AngularEshop.DataLayer.Entities.Account;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace AngularEshop.Core.Services.Implementations
{
    public class AccessService : IAccessService
    {
        #region constructor
        private readonly IGenericRepository<Role> roleRepository;
        private readonly IGenericRepository<UserRole> userRoleRepository;
        private readonly IGenericRepository<User> userRepository;
        public AccessService(IGenericRepository<Role> roleRepository,
            IGenericRepository<UserRole> userRoleRepository,
            IGenericRepository<User> userRepository)
        {
            this.roleRepository = roleRepository;
            this.userRoleRepository = userRoleRepository;
            this.userRepository = userRepository;
        }
        #endregion

        #region user role
        public async Task<bool> CheckUserRole(long userId, string role)
        {
            return await userRoleRepository.GetEntitiesQuery().AsQueryable()
                .AnyAsync(s => s.UserId == userId && s.Role.Name == role);
        }
        #endregion

        #region dispose
        public void Dispose()
        {
            userRepository?.Dispose();
            userRoleRepository?.Dispose();
            userRepository?.Dispose();
        }
        #endregion
    }
}
