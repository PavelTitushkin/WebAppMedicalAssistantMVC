using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class DoctorVisitController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDoctorVisitService _doctorVisitService;

        public DoctorVisitController(IMapper mapper, IDoctorVisitService doctorVisitService)
        {
            _mapper = mapper;
            _doctorVisitService = doctorVisitService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var listDoctorVisits = await _doctorVisitService.GetAllDoctorVisitAsync();
                if(listDoctorVisits.Any())
                {
                    return View(listDoctorVisits);
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
