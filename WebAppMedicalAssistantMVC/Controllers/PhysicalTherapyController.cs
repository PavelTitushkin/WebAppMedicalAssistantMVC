using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class PhysicalTherapyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPhysicalTherapyService _physicalTherapyService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;

        public PhysicalTherapyController(IMapper mapper, IPhysicalTherapyService physicalTherapyService)
        {
            _mapper = mapper;
            _physicalTherapyService = physicalTherapyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
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
        public async Task<IActionResult> Create(PhysicalTherapyModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var dto = _mapper.Map<PhysicalTherapyDto>(model);
                    await _physicalTherapyService.CreatePhysicalTherapyAsync(dto);

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

    }
}
