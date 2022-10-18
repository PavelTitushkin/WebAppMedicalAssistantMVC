using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class FluorographyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFluorographyService _fluorographyService;
        private readonly IUserService _userService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;
        public FluorographyController(IFluorographyService fluorographyService, IMapper mapper, IUserService userService, IMedicalInstitutionService medicalInstitutionService)
        {
            _fluorographyService = fluorographyService;
            _mapper = mapper;
            _userService = userService;
            _medicalInstitutionService = medicalInstitutionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity?.Name;
                var dtoUser = await _userService.GetUserByEmailAsync(emailUser);
                var fluorographies = await _fluorographyService.GetAllFluorographiesAsync(dtoUser.Id);

                return View(fluorographies);
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
                var medicalInstitutions = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var fluorographyModel = new FluorographyModel();
                fluorographyModel.MedicalInstitutionList = new SelectList(medicalInstitutions, "Id", "NameMedicalInstitution");

                return View(fluorographyModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(FluorographyModel fluorographyModel)
        {
            try
            {
                var userEmail = HttpContext.User.Identity.Name;
                var dtoUser = await _userService.GetUserByEmailAsync(userEmail);
                fluorographyModel.UserId = dtoUser.Id;
                var fluorographyDto = _mapper.Map<FluorographyDto>(fluorographyModel);
                await _fluorographyService.CreatefluorographyAsync(fluorographyDto);

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
