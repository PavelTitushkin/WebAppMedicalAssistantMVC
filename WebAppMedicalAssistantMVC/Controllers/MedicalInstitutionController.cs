using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize]
    public class MedicalInstitutionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMedicalInstitutionService _medicalInstitutionService;

        public MedicalInstitutionController(IMapper mapper, IMedicalInstitutionService medicalInstitutionService)
        {
            _mapper = mapper;
            _medicalInstitutionService = medicalInstitutionService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var model = new MedicalInstitutionModel();
                model.ReturnUrl = Request.Headers["Referer"].ToString();

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
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
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsMedicalInstitutionPartialView(int id)
        {
            try
            {
                var dto = await _medicalInstitutionService.GetByIdMedicalInstitutionAsync(id);

                return PartialView(dto);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }
    }
}
