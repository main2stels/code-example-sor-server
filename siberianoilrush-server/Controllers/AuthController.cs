using DB.Mongo.Model.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using siberianoilrush_server.Auth;
using siberianoilrush_server.Extension;
using siberianoilrush_server.Model;
using siberianoilrush_server.Security;
using siberianoilrush_server.Service;
using siberianoilrush_server.Service.Inventory;
using siberianoilrush_server.Service.Public;
using SorResources.Models.Enums;
using SorResources.Models.Types;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace siberianoilrush_server.Controllers
{
    [Route("api/auth")]
    public class AuthController: Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IWriteWalletService _walletService;
        private readonly ISquadService _squadService;
        private readonly IOilfieldService _oilfieldService;
        private readonly IExtractionService _extractionService;
        private readonly IUserInfoService _userInfoService;
        private readonly ILimitService _limitService;

        public AuthController(IUserService userService,
            IEmailService emailService,
            IWriteWalletService walletService,
            ISquadService squadService,
            IOilfieldService oilfieldService,
            IExtractionService extractionService,
            IUserInfoService userInfoService,
            ILimitService limitService)
        {
            _userService = userService;
            _emailService = emailService;
            _squadService = squadService;
            _walletService = walletService;
            _oilfieldService = oilfieldService;
            _extractionService = extractionService;
            _userInfoService = userInfoService;
            _limitService = limitService;
        }

        [HttpPost]
        public IActionResult Registration(UserModel userModel)
        {
            string info = "";
            if (!(userModel?.IsValid(out info) ?? false))
                return BadRequest(info);

            if (_userService.GetByNickname(userModel.Nickname) != null)
                return Error("Введенный никнейм уже используется, введите другой");

            if (_userService.GetByEmail(userModel.Email) != null)
                return Error("Введенный email уже используется, введите другой");

            var user = _userService.Create(userModel);

            SetSquad(user.Id);
            SetOilfield(user.Id);

            foreach (var c in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
            {
                long value = 0;

                if (c == ResourceType.Money)
                    value = 50000;
                else if (c == ResourceType.Diesel)
                    value = 5000;

                _walletService.Save(new Wallet
                {
                    Id = ObjectId.GenerateNewId(),
                    UserId = user.Id,
                    Currency = c,
                    Amount = value
                });
            }

            _extractionService.Instantiate(user.Id);
            _userInfoService.Instantiate(user.Id);
            _limitService.Instantiate(user.Id);

            return Ok();
        }

        [HttpPost]
        public IActionResult Authorization(LoginModel loginModel)
        {
            string info = "";
            if (!(loginModel?.IsValid(out info) ?? false))
                return Error(info);

            loginModel.Login = loginModel.Login.ToLower();

            var user = _userService.GetByLogin(loginModel.Login);

            if (user == null || !AuthSecurity.IsPasswordValid(loginModel.Password, user.Salt, user.Hash))
                return Error("Неверный логин или пароль");

            var token = GetIdentity(user.Email, "user");

            _userInfoService.UpdateTimeLastGame(user.Id);

            return Json(new SorResources.Models.Auth.AuthorizationModel { Token = token });
        }

        [HttpGet]
        [Authorize]
        public IActionResult IsValid()
            => Ok();

        private IActionResult Error(string message)
        {
            var result = Json(new { result = message });
            result.StatusCode = 400;

            return result;
        }

        private void SetSquad(ObjectId userId)
        {
            int count = 1;
            var sSquads = _squadService.GetAllStatic().ToDictionary(x => count++, x=> x);

            for (int i = 0; i < 6; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    _squadService.Create(userId, sSquads[j]);
                }
            }
        }

        private void SetOilfield(ObjectId userId)
        {
            var oilfield = _oilfieldService.Create(userId);

            var trees = _oilfieldService.CreateTrees(userId);
        }

        private string GetIdentity(string username, string role)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };

            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.Now,
                    claims: claimsIdentity.Claims,
                    expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
