using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Core.DTOs.Account;
using AngularEshop.DataLayer.Entities.Account;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<List<User>> GetAllUsers();

        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        bool IsUserExistsByEmail(string email);
        Task<LoginUserResult> LoginUser(LoginUserDTO login, bool checkAdminRole = false);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(long userId);
        Task<User> GetUserByEmailActiveCode(string emailActiveCode);
        void ActivateUser(User user);
        Task EditUserInfo(EditUserDTO user, long userId);
        Task<bool> IsUserAdmin(long userId);
    }
}
