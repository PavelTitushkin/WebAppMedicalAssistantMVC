using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class MedicalInstitutionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMedicalInstitutionService _medicalInstitutionService;
        private readonly IUserService _userService;

        public MedicalInstitutionController(IMapper mapper, IMedicalInstitutionService medicalInstitutionService, IUserService userService)
        {
            _mapper = mapper;
            _medicalInstitutionService = medicalInstitutionService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var model = new MedicalInstitutionModel();
                model.ReturnUrl = Request.Headers["Referer"].ToString();

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicalInstitutionModel model)
        {
            try
            {
                var dto = _mapper.Map<MedicalInstitutionDto>(model);
                await _medicalInstitutionService.CreateMedicalInstitutionAsync(dto);

                return Redirect(model.ReturnUrl);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
