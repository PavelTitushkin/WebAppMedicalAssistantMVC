using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using System.Security.Cryptography.Xml;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
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
        public async Task<IActionResult> Index(int pageIndex, DateTime SearchDateStart, DateTime SearchDateEnd, bool AllDates)
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                if (!AllDates)
                {
                    var listDoctorVisits = await _doctorVisitService.GetAllDoctorVisitAsync(userDto.Id);
                    if (pageIndex == 0)
                    {
                        pageIndex = 1;
                    }
                    var model = PagingList.Create(listDoctorVisits, 5, pageIndex);

                    return View(model);
                }
                else
                {
                    var listDoctorVisits = await _doctorVisitService.GetPeriodDoctorVisitAsync(SearchDateStart, SearchDateEnd, userDto.Id);
                    var model = PagingList.Create(listDoctorVisits, 5, pageIndex);

                    return View(model);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> IndexByTransferredDisease(int id, int pageIndex)
        {
            try
            {
                var dto = await _doctorVisitService.GetDoctorVisitByIdTransferredDiseaseAsync(id);
                if (pageIndex == 0)
                {
                    pageIndex = 1;
                }
                var model = PagingList.Create(dto, 5, pageIndex);

                return View(model);
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

                var model = new DoctorVisitModel();
                model.MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution");
                model.DoctorList = new SelectList(doctorsDto, "Id", "FullNameDoctor");
                if (model.ReturnUrl == "https://localhost:7068/DoctorVisit/CreateDoctor")
                {
                    model.ReturnUrl = "https://localhost:7068/DoctorVisit/Index";
                }
                if (model.ReturnUrl == "https://localhost:7068/MedicalInstitution/Create")
                {
                    model.ReturnUrl = "https://localhost:7068/DoctorVisit/Index";
                }


                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateWithTransferredDisease(int id)
        {
            try
            {
                var doctorsDto = await _doctorService.GetAllDoctorAsync();
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var model = new DoctorVisitModel()
                {
                    TransferredDiseaseId = id,
                    MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution"),
                    DoctorList = new SelectList(doctorsDto, "Id", "FullNameDoctor")
                };
                if (model.ReturnUrl == "https://localhost:7068/DoctorVisit/CreateDoctor")
                {
                    model.ReturnUrl = "https://localhost:7068/DoctorVisit/Index";
                }
                if (model.ReturnUrl == "https://localhost:7068/MedicalInstitution/Create")
                {
                    model.ReturnUrl = "https://localhost:7068/DoctorVisit/Index";
                }

                return View(model);
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
        public IActionResult Delete(int id, DateTime dateVisit, string nameMedicalInstitution, string nameOfDisease)
        {
            try
            {
                var model = new DoctorVisitModel();
                model.DateVisit = dateVisit;
                model.NameMedicalInstitution = nameMedicalInstitution;
                model.NameOfDisease = nameOfDisease;
                model.Id = id;

                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _doctorVisitService.DeleteDoctorVisitAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id, DateTime dateVisit, string nameMedicalInstitution, string fullNameDoctor, decimal priceVisit)
        {
            try
            {
                var doctorsDto = await _doctorService.GetAllDoctorAsync();
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var model = new DoctorVisitModel();
                model.Id = id;
                model.DateVisit = dateVisit;
                model.MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution", nameMedicalInstitution);
                model.DoctorList = new SelectList(doctorsDto, "Id", "FullNameDoctor", fullNameDoctor);
                model.PriceVisit = priceVisit;
                model.ReturnUrl = Request.Headers["Referer"].ToString();
                if (model.ReturnUrl == "https://localhost:7068/DoctorVisit/CreateDoctor")
                {
                    model.ReturnUrl = "https://localhost:7068/DoctorVisit/Index";
                }
                if (model.ReturnUrl == "https://localhost:7068/MedicalInstitution/Create")
                {
                    model.ReturnUrl = "https://localhost:7068/DoctorVisit/Index";
                }

                return View(model);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorVisitModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var dto = await _doctorVisitService.GetDoctorVisitByIdAsync(model.Id);
                    dto.DateVisit = model.DateVisit;
                    dto.MedicalInstitutionId = model.MedicalInstitutionId;
                    dto.DoctorId = model.DoctorId;
                    dto.PriceVisit = model.PriceVisit;
                    await _doctorVisitService.UpdateDoctorVisitAsync(dto, dto.Id);

                    return Redirect(model.ReturnUrl);
                }

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult AddOrEditDescriptionAppointment(int id)
        {
            try
            {
                var model = new AppointmentModel();
                model.Id = id;
                model.ReturnUrl = Request.Headers["Referer"].ToString();

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditDescriptionAppointment(AppointmentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = await _doctorVisitService.GetAppointmentAsync(model.Id);
                    dto.DescriptionOfDestination = model.DescriptionOfDestination;
                    await _doctorVisitService.UpdateAppointmentAsync(dto, dto.Id);

                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return View(model);
                }
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

        [HttpGet]
        public IActionResult CreateDoctor()
        {
            try
            {
                var model = new DoctorModel();
                model.ReturnUrl = Request.Headers["Referer"].ToString();

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(DoctorModel model)
        {
            try
            {
                var dto = _mapper.Map<DoctorDto>(model);
                await _doctorService.CreateDoctorAsync(dto);

                return Redirect(model.ReturnUrl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsDoctorPartialView(int id)
        {
            try
            {
                var dto = await _doctorService.GetDoctorByIdAsync(id);

                return PartialView(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
