using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AccountController(IMapper mapper, IUserService userService, IRoleService roleService)
        {
            _mapper = mapper;
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserEmail()
        {
            try
            {
                var userEmail = HttpContext.User.Identity?.Name;
                if (string.IsNullOrEmpty(userEmail))
                {
                    return BadRequest();
                }
                var userDto = await _userService.GetUserByEmailAsync(userEmail);
                var userModel = _mapper.Map<UserData>(userDto);

                return View(userModel);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                var isPasswordCorrect = await _userService.CheckUserPasswordAsync(loginModel.Email, loginModel.Password);
                if (isPasswordCorrect)
                {
                    var roleName = await Authenticate(loginModel.Email);
                    if (roleName == "admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckEmail(string email)
        {
            try
            {
                var isUserExist = await _userService.IsUserExistAsync(email);
                if (isUserExist)
                {
                    return Ok(false);
                }

                return Ok(true);

            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userRoleId = await _roleService.GetRoleIdByNameAsync("User");
                    var userDto = _mapper.Map<UserDto>(registerModel);
                    if (userDto != null && userRoleId != null)
                    {
                        userDto.RoleId = userRoleId.Value;
                        var result = await _userService.RegisterUser(userDto);
                        if (result > 0)
                        {
                            var roleName = await Authenticate(registerModel.Email);
                            if (roleName == "admin")
                            {
                                return RedirectToAction("Index", "Admin");
                            }

                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                return View(registerModel);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        private async Task<string> Authenticate(string email)
        {
            try
            {
                var userDto = await _userService.GetUserByEmailAsync(email);

                var claims = new List<Claim>()
                {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userDto.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userDto.RoleName)
                };

                var identity = new ClaimsIdentity(
                    claims,
                    "ApplicationCookie",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType
                    );

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return userDto.RoleName;

            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Exit()
        {
            try
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }
    }
}
