using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;
using System.IO;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IDoctorVisitService _doctorVisitService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IDoctorVisitService doctorVisitService)
        {
            _logger = logger;
            _userService = userService;
            _doctorVisitService = doctorVisitService;
        }

        public async Task<IActionResult> Index()
        {
            var dateNow = DateTime.Now;
            var dto = await _doctorVisitService.GetScheduledDoctorVisitAsync(dateNow);

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrEditUserInfo()
        {
            try
            {
                var userEmail = HttpContext.User.Identity?.Name;
                var userDto = await _userService.GetUserByEmailAsync(userEmail);
                var model = new UserModel()
                {
                    LastName = userDto.LastName,
                    FirstName = userDto.FirstName,
                    Birthday = userDto.Birthday,
                    AvatarByte = userDto.Avatar,
                    Email = userDto.Email,
                    Id = userDto.Id,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEditAvatar()
        {

            try
            {
                var userEmail = HttpContext.User.Identity?.Name;
                var userDto = await _userService.GetUserByEmailAsync(userEmail);
                var model = new UserModel()
                {
                    Email = userDto.Email,
                    AvatarByte = userDto.Avatar,
                    Id = userDto.Id,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditAvatar(UserModel model)
        {
            try
            {
                if(model.Avatar !=null)
                {
                    var dto = await _userService.GetUserByEmailAsync(model.Email);
                    byte[] imageData = null;
                    using (BinaryReader binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    dto.Avatar = imageData;

                    await _userService.UpdateUserAsync(dto, dto.Id);
                }

                return RedirectToAction("GetOrEditUserInfo");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInfo(UserModel model)
        {
            try
            {
                var dto = await _userService.GetUserByEmailAsync(model.Email);
                dto.LastName = model.LastName;
                dto.FirstName = model.FirstName;
                dto.Birthday = model.Birthday;
                dto.Email = model.Email;
                if(dto.Avatar?.Length == 0)
                {
                    byte[] imageData = null;
                    using (BinaryReader binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    dto.Avatar = imageData;
                }

                await _userService.UpdateUserAsync(dto, dto.Id);

                return RedirectToAction("GetOrEditUserInfo");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}