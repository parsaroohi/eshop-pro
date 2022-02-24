using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Core.DTOs.Account;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using AngularEshop.Core.Utilities.Extensions.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AngularEshop.WebApi.Controller
{
    public class AdminAccountController : SiteBaseController
    {
        #region coustructor
        private IUserService userService;
        public AdminAccountController(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var res = await userService.LoginUser(login);
                switch (res)
                {
                    case LoginUserResult.IncorrectData:
                        return JsonResponseStatus.NotFound();
                    case LoginUserResult.NotActivated:
                        return JsonResponseStatus.Error(new { message = "حساب کاربری شما فعال نشده است" });
                    case LoginUserResult.NotAdmin:
                        return JsonResponseStatus.Error(new { message = "شما به این بخش دسترسی ندارید" });
                    case LoginUserResult.Success:
                        var user = await userService.GetUserByEmail(login.Email);
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEshopJwtBearer"));
                        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOptions = new JwtSecurityToken(
                            issuer: "https://localhost:44381",
                            claims: new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,user.Email),
                                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                            },
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: signingCredentials
                            );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        return JsonResponseStatus.Success(new
                        {
                            token = tokenString,
                            expireTime = 30,
                            firstName = user.FirstName,
                            lastName = user.LastName,
                            userId = user.Id
                        });
                }
            }
            return JsonResponseStatus.Error();
        }
        #endregion

        #region check user admin authentication
        [HttpPost("check-admin-auth")]
        public async Task<IActionResult> CheckUserAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();
                var user = await userService.GetUserById(userId);
                if (await userService.IsUserAdmin(userId))
                {
                    return JsonResponseStatus.Success(new
                    {
                        userId = user.Id,
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        address = user.Address,
                        email = user.Email
                    });
                }
            }
            return JsonResponseStatus.Error();
        }
        #endregion
    }
}
