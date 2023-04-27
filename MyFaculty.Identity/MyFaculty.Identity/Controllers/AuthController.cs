using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Identity.Models;
using MyFaculty.Identity.Services;
using MyFaculty.Identity.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Unidecode.NET;

namespace MyFaculty.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IConfiguration _configuration;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService interactionService, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel viewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Введенные Вами данные некорректны.";
                return View(viewModel);
            }
            AppUser user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user == null)
            {
                viewModel.ErrorMessage = "Пользователь с введенными параметрами не найден.";
                return View(viewModel);
            }
            if (user.IsBanned)
            {
                viewModel.ErrorMessage = "Пользователь с введенными параметрами заблокирован за нарушение правил сайта.";
                return View(viewModel);
            }
            var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }
            viewModel.ErrorMessage = "Пользователь с введенными параметрами не найден.";
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            RegisterViewModel viewModel = new RegisterViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Введенные Вами данные некорректны.";
                return View(viewModel);
            }
            AppUser user = new AppUser()
            {
                UserName = (viewModel.FirstName + viewModel.LastName).Unidecode(),
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email
            };
            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, false);
                return Redirect(viewModel.ReturnUrl);
            }
            viewModel.ErrorMessage = result.Errors.First().Description;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        [HttpGet]
        public IActionResult ForgotPassword(string returnUrl)
        {
            ForgotPasswordViewModel viewModel = new ForgotPasswordViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(viewModel.Email);
                if (user == null)
                {
                    viewModel.ErrorMessage = "Произошла ошибка. Действие невозможно.";
                    return View(viewModel);
                }
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                string callbackUri = Url.Action("ResetPassword", "Auth", new
                {
                    userId= user.Id,
                    code = code
                });
                EmailService emailService = new EmailService(_configuration);
                await emailService.SendEmailAsync(viewModel.Email, "Восстановление пароля", $"Для сброса пароля пройдите по <a href='{_configuration["BaseUrl"] + callbackUri}'>ссылке</a>.");
                return View("ForgotPasswordConfirmation");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ResetPassword(string code, string returnUrl)
        {
            return code == null ? View("Error") : View(new ResetPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            AppUser user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user == null)
            {
                viewModel.ErrorMessage = "Произошла ошибка. Действие невозможно.";
                return View(viewModel);
            }
            var result = await _userManager.ResetPasswordAsync(user, viewModel.Code, viewModel.Password);
            if (result.Succeeded)
                return View("SuccessPasswordReset");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(viewModel);
        }
    }
}
