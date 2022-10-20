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
        private readonly IMedicalInstitutionService _medicalInstitutionService;

        public MedicalExaminationController(IMapper mapper, IMedicalExaminationService medicalExaminationService, IUserService userService, IMedicalInstitutionService medicalInstitutionService)
        {
            _mapper = mapper;
            _medicalExaminationService = medicalExaminationService;
            _userService = userService;
            _medicalInstitutionService = medicalInstitutionService;
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

        //[HttpGet]
        //public async Task<IActionResult> IndexPhysicalTherapy(int id)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> CreatePhysicalTherapy(int id)
        {
            try
            {
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var physicalTherapyModel = new PhysicalTherapyModel();
                physicalTherapyModel.AppointmentId = id;
                physicalTherapyModel.MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution");
                physicalTherapyModel.ReturnUrl = Request.Headers["Referer"].ToString();

                return View(physicalTherapyModel);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhysicalTherapy(PhysicalTherapyModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var dto = _mapper.Map<PhysicalTherapyDto>(model);
                    await _medicalExaminationService.CreatePhysicalTherapyAsync(dto);

                    var returnUrl = model.ReturnUrl;

                    return Redirect(returnUrl);
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
        public async Task<IActionResult> Create(int id)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
