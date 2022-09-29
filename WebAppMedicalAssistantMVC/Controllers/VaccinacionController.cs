using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class VaccinacionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVaccinationService _vaccinationService;

        public VaccinacionController(IMapper mapper, IVaccinationService vaccinationService)
        {
            _mapper = mapper;
            _vaccinationService = vaccinationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var listVaccinations = await _vaccinationService.GetAllVaccinationsAsync();
                if(listVaccinations.Any())
                {
                    return View(listVaccinations);
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
