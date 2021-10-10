using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Register
        //Высылает представление
        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        //Принимает представление
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User 
            { 
                UserName = model.UserName
            };

            var register = await _userManager.CreateAsync(user, model.Password);

            if (register.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in register.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }
        #endregion

        #region Login
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl});
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var login = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (login.Succeeded)
            {
                //return Redirect(model.ReturnUrl); // не безопасно!

                //if (Url.IsLocalUrl(model.ReturnUrl)) //Как должно быть
                //    return Redirect(model.ReturnUrl);
                //return RedirectToAction("Index", "Home");

                return LocalRedirect(model.ReturnUrl ?? "/");
            }

            ModelState.AddModelError("", "Ошибка ввода имени пользователя или пароля!");

            return View(model);
        }
        #endregion

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
