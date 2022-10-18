using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAnalysisService _analysisService;
        private readonly IUserService _userService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;

        public AnalysisController(IMapper mapper, IAnalysisService analysisService, IUserService userService, IMedicalInstitutionService medicalInstitutionService)
        {
            _mapper = mapper;
            _analysisService = analysisService;
            _userService = userService;
            _medicalInstitutionService = medicalInstitutionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity?.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var listAnalisis = await _analysisService.GetAllAnalysisAsync(userDto.Id);

                return View(listAnalisis);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            try
            {
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var analysisModel = new AnalysisModel();
                analysisModel.AppointmentId = id;
                analysisModel.MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution");

                return View(analysisModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnalysisModel analysisModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    var emailUser = HttpContext.User.Identity.Name;
                    var userDto = await _userService.GetUserByEmailAsync(emailUser);
                    analysisModel.UserId = userDto.Id;

                    var analysisDto = _mapper.Map<AnalysisDto>(analysisModel);
                    await _analysisService.CreateAnalysisAsync(analysisDto);
                    
                    return Redirect(Request.GetTypedHeaders().Referer.ToString());
                }
                else
                {
                    return View(analysisModel);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
