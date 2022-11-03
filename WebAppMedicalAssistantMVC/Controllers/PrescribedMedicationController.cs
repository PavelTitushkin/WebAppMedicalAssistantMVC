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
    //[Authorize(Roles = "user")]
    [Authorize]
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

        [HttpGet]
        public IActionResult Create(int? id)
        {
            try
            {
                var model = new PrescribedMedicationModel();
                model.AppointmentId = id;
                model.ReturnUrl = Request.Headers["Referer"].ToString();

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PrescribedMedicationModel model)
        {
            try
            {
                var userEmail = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(userEmail);
                var dto = _mapper.Map<PrescribedMedicationDto>(model);
                //await _prescribedMedicationService.CreatePrescribedMedication(dto);

                return Redirect(model.ReturnUrl);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
