using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
    public class MedicalExaminationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMedicalExaminationService _medicalExaminationService;
        private readonly IUserService _userService;

        public MedicalExaminationController(IMapper mapper, IMedicalExaminationService medicalExaminationService, IUserService userService)
        {
            _mapper = mapper;
            _medicalExaminationService = medicalExaminationService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var listMedicalExaminations = await _medicalExaminationService.GetAllMedicalExaminationAsync(userDto.Id);

                return View(listMedicalExaminations);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
