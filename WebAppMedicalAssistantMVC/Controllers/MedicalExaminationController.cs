using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
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
