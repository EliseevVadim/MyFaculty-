using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Identity.Models;
using MyFaculty.Identity.ViewModels;
using System.Threading.Tasks;
using Unidecode.NET;

namespace MyFaculty.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
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
            viewModel.ErrorMessage = "Произошла ошибка. Попробуйте снова";
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
