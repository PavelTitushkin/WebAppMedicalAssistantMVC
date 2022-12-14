using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IDoctorVisitService _doctorVisitService;
        private readonly IAnalysisService _analysisService;
        private readonly IMedicalExaminationService _medicalExaminationService;
        private readonly IPhysicalTherapyService _physicalTherapyService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IDoctorVisitService doctorVisitService, IAnalysisService analysisService, IMedicalExaminationService medicalExaminationService, IPhysicalTherapyService physicalTherapyService)
        {
            _logger = logger;
            _userService = userService;
            _doctorVisitService = doctorVisitService;
            _analysisService = analysisService;
            _medicalExaminationService = medicalExaminationService;
            _physicalTherapyService = physicalTherapyService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity?.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var dateNow = DateTime.Now;
                var dtoDoctorVisit = await _doctorVisitService.GetScheduledDoctorVisitAsync(dateNow, userDto.Id);
                var dtoAnalysis = await _analysisService.GetScheduledAnalysisAsync(dateNow, userDto.Id);
                var dtoMedicalExamination = await _medicalExaminationService.GetScheduledMedicalExaminationAsync(dateNow, userDto.Id);
                var dtoPhysicalTherapy = await _physicalTherapyService.GetScheduledPhysicalTherapyAsync(dateNow, userDto.Id);

                var model = new CalendarViewModel()
                {
                    DoctorVisits = dtoDoctorVisit,
                    AnalysisVisits = dtoAnalysis,
                    MedicalExaminationVisits = dtoMedicalExamination,
                    PhysicalTherapyVisits = dtoPhysicalTherapy,
                };

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
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
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
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
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditAvatar(UserModel model)
        {
            try
            {
                if (model.Avatar != null)
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
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
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
                if (dto.Avatar?.Length == 0)
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
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
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