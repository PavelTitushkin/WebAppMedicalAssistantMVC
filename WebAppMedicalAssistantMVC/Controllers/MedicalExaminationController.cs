using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class MedicalExaminationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMedicalExaminationService _medicalExaminationService;

        public MedicalExaminationController(IMapper mapper, IMedicalExaminationService medicalExaminationService)
        {
            _mapper = mapper;
            _medicalExaminationService = medicalExaminationService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var listMedicalExaminations = await _medicalExaminationService.GetAllMedicalExaminationAsync();
                if(listMedicalExaminations.Any())
                {
                    return View(listMedicalExaminations);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
