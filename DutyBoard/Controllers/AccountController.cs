using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using DutyBoard_Models.ViewModels;
using DutyBoard_Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using DutyBoard_Models.Account;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Telegram.Bot.Types;

namespace DutyBoard.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISiteUserRepository _userRepo;

        public AccountController(ISiteUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                //User user = Models.User.GetAllUser().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (_userRepo.CheckUser(model.LoginName, model.Password))
                {
                    await Authenticate(model.LoginName); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                TempData[WC.Error] = "Некорректные логин или пароль";
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SiteUser model)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepo.FirstOrDefault(model.LoginName);
                if (user != null)
                {
                    TempData[WC.Error] = "Пользователь с таким логином уже зарегистрирован.";
                }
                else
                {
                    // добавляем пользователя в бд

                    _userRepo.Upsert(new SiteUser()
                    {
                        LoginName = model.LoginName,
                        Password = model.Password
                    });

                    //await Authenticate(model.Login); // аутентификация
                    TempData[WC.Success] = "Регистрация завершена.";
                    TempData[WC.Error] = "Авторизация не выполнена!\nНеобхрдимо активировать аккаунт.";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                var errorMessage = ModelState.Values.FirstOrDefault(x => x.ValidationState == ModelValidationState.Invalid)
                    .Errors[0].ErrorMessage;
                TempData[WC.Error] = errorMessage;
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
