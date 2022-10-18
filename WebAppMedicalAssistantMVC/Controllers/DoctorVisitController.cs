using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class DoctorVisitController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDoctorVisitService _doctorVisitService;
        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;

        public DoctorVisitController(IMapper mapper, IDoctorVisitService doctorVisitService, IUserService userService, IDoctorService doctorService, IMedicalInstitutionService medicalInstitutionService)
        {
            _mapper = mapper;
            _doctorVisitService = doctorVisitService;
            _userService = userService;
            _doctorService = doctorService;
            _medicalInstitutionService = medicalInstitutionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var listDoctorVisits = await _doctorVisitService.GetAllDoctorVisitAsync(userDto.Id);
                
                return View(listDoctorVisits);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var doctorsDto = await _doctorService.GetAllDoctorAsync();
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var doctorVisitModel = new DoctorVisitModel();
                doctorVisitModel.MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution");
                doctorVisitModel.DoctorList = new SelectList(doctorsDto, "Id", "FullNameDoctor");

                return View(doctorVisitModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorVisitModel doctorVisitModel)
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                doctorVisitModel.UserId = userDto.Id;

                var doctorVisitDto = _mapper.Map<DoctorVisitDto>(doctorVisitModel);
                await _doctorVisitService.CreateDoctorVisitAsync(doctorVisitDto);

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Appointment(int id, string? nameOfDisease)
        {
            try
            {
                var appointmentDto = await _doctorVisitService.GetAppointmentAsync(id);
                ViewBag.NameOfDisease = nameOfDisease;

                return View(appointmentDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateAppointment(int id, string? nameOfDisease)
        {
            try
            {
                await _doctorVisitService.CreateAppointment(id);

                return RedirectToAction("Appointment", new {id, nameOfDisease});
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
