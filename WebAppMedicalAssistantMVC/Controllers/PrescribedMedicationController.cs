using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class PrescribedMedicationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPrescribedMedicationService _prescribedMedicationService;
        private readonly IUserService _userService;

        public PrescribedMedicationController(IMapper mapper, IPrescribedMedicationService prescribedMedicationService, IUserService userService)
        {
            _mapper = mapper;
            _prescribedMedicationService = prescribedMedicationService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity?.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var listPrescribedMedications = await _prescribedMedicationService.GetAllPrescribedMedicationsAsync(userDto.Id);

                return View(listPrescribedMedications);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
