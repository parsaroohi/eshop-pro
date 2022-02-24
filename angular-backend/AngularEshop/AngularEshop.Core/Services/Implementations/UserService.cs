using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Core.DTOs.Account;
using AngularEshop.Core.Security;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Convertors;
using AngularEshop.DataLayer.Entities.Access;
using AngularEshop.DataLayer.Entities.Account;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace AngularEshop.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        #region constructor
        private IGenericRepository<User> _userRepository;
        private IPasswordHelper passwordHelper;
        private IMailSender mailSender;
        private IViewRenderService renderService;
        private IGenericRepository<UserRole> userRoleRepository;
        public UserService(IGenericRepository<User> userRepository,
            IPasswordHelper passwordHelper,
            IMailSender mailSender,
            IViewRenderService renderService,
            IGenericRepository<UserRole> userRoleRepository)
        {
            this._userRepository = userRepository;
            this.passwordHelper = passwordHelper;
            this.mailSender = mailSender;
            this.renderService = renderService;
            this.userRoleRepository = userRoleRepository;
        }
        #endregion

        #region user section
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetEntitiesQuery().ToListAsync();
        }
        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
        {
            if (IsUserExistsByEmail(register.Email))
            {
                return RegisterUserResult.Success;

            }

            var user = new User()
            {
                Email = register.Email.SanitizeText(),
                FirstName = register.FirstName.SanitizeText(),
                Address = register.Address.SanitizeText(),
                LastName = register.LastName.SanitizeText(),
                EmailActiveCode = Guid.NewGuid().ToString(),
                Password = passwordHelper.EncodePasswordMd5(register.Password)
            };

            await _userRepository.AddEntity(user);
            await _userRepository.SaveChanges();

            var body = await renderService.RenderToStringAsync("Email/ActivateAccount", user);
            mailSender.Send("test@test.com", "تست فعالسازی", body);

            return RegisterUserResult.Success;
        }

        public bool IsUserExistsByEmail(string email)
        {
            return _userRepository.GetEntitiesQuery().Any(s => s.Email == email.ToLower().Trim());
        }

        public async Task<LoginUserResult> LoginUser(LoginUserDTO login, bool checkAdminRole = false)
        {
            var password = passwordHelper.EncodePasswordMd5(login.Password);
            var user = await _userRepository.GetEntitiesQuery()
                .SingleOrDefaultAsync(s => s.Email == login.Email.ToLower().Trim()
            && s.Password == password);
            if (user == null) return LoginUserResult.IncorrectData;

            if (!user.IsActivated) return LoginUserResult.NotActivated;

            if (checkAdminRole)
            {
                if (!await IsUserAdmin(user.Id)) return LoginUserResult.NotAdmin;
            }

            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetEntitiesQuery()
                .SingleOrDefaultAsync(s => s.Email == email.ToLower().Trim());
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _userRepository.GetEntityById(userId);
        }
        #endregion

        #region dispose
        public void Dispose()
        {
            _userRepository?.Dispose();
        }
        #endregion

        #region get and edit
        public Task<User> GetUserByEmailActiveCode(string emailActiveCode)
        {
            return _userRepository.GetEntitiesQuery()
                .SingleOrDefaultAsync(s => s.EmailActiveCode == emailActiveCode);
        }

        public void ActivateUser(User user)
        {
            user.IsActivated = true;
            user.EmailActiveCode = Guid.NewGuid().ToString();
            _userRepository.UpdateEntity(user);
            _userRepository.SaveChanges();
        }

        public async Task EditUserInfo(EditUserDTO user, long userId)
        {
            var mainUser = await _userRepository.GetEntityById(userId);
            if (mainUser != null)
            {
                mainUser.FirstName = user.FirstName;
                mainUser.LastName = user.LastName;
                mainUser.Address = user.Address;
                _userRepository.UpdateEntity(mainUser);
                await _userRepository.SaveChanges();
            }
        }
        #endregion

        #region check admin
        public async Task<bool> IsUserAdmin(long userId)
        {
            return await userRoleRepository.GetEntitiesQuery()
                    .Include(s => s.Role)
                    .AsQueryable().AnyAsync(s => s.UserId == userId && s.Role.Name == "admin");
        }
        #endregion
    }
}
