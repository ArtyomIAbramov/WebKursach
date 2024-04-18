using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebKursach.ApplicationCore.Models;
using WebKursach.Infrastructure.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPILab2.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/account/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (ModelState.IsValid)
            {

                User user = new() { Email = model.Email, UserName = model.Email };
                // Добавление нового пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogExtension("Created User", user);
                    await _userManager.AddToRoleAsync(user, "user");
                    // Установка куки
                    await _signInManager.SignInAsync(user, false);
                    _logger.LogExtension("Sign in User", user);
                    return Ok(new { message = "Добавлен новый пользователь: " + user.UserName });
                }
                else
                {
                    _logger.LogExtension("Error created User", user, LogLevel.Error);
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    var errorMsg = new
                    {
                        message = "Пользователь не добавлен",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return BadRequest();
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Неверные входные данные",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                _logger.LogExtension("incorrect input data","",LogLevel.Error);
                return BadRequest();

            }
        }

        [HttpPost]
        [Route("api/account/login")]
        //[AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            if (ModelState.IsValid)
            {
                var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    User? user = await _userManager.FindByEmailAsync(model.Email);
                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    string? userRole = roles.FirstOrDefault();

                    _logger.LogExtension("Entered User", user);
                    return Ok(new { message = "Выполнен вход", userName = model.Email, userRole });
                }
                else
                {
                    _logger.LogExtension("incorrect username or password", "", LogLevel.Error);
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    var errorMsg = new
                    {
                        message = "Вход не выполнен",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return BadRequest();
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                _logger.LogExtension("Error", errorMsg, LogLevel.Error);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/account/logoff")]
        public async Task<IActionResult> LogOff()
        {
            User usr = await GetCurrentUserAsync();
            if (usr == null)
            {
                _logger.LogExtension("Need to enter", "", LogLevel.Warning);
                return Unauthorized(new { message = "Сначала выполните вход" });
            }
            // Удаление куки
            await _signInManager.SignOutAsync();
            _logger.LogExtension("Log out");
            return Ok(new { message = "Выполнен выход", userName = usr.UserName });
        }

        [HttpGet]
        [Route("api/account/isauthenticated")]
        public async Task<IActionResult> IsAuthenticated()
        {
            User usr = await GetCurrentUserAsync();
            if (usr == null)
            {
                _logger.LogExtension("You are guest, need to enter", "", LogLevel.Warning);
                return Unauthorized(new { message = "Вы Гость. Пожалуйста, выполните вход" });
            }
            IList<string> roles = await _userManager.GetRolesAsync(usr);
            string? userRole = roles.FirstOrDefault();
            _logger.LogExtension("Session activated for", usr);
            return Ok(new { message = "Сессия активна", userName = usr.UserName, userRole });
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
