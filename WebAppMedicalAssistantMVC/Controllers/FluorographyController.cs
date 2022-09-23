using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class FluorographyController : Controller
    {
        private readonly IFluorographyService _fluorographyService;
        private readonly IMapper _mapper;

        public FluorographyController(IFluorographyService fluorographyService, IMapper mapper)
        {
            _fluorographyService = fluorographyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var fluorographies = await _fluorographyService.GetAllFluorographiesAsync();
                if (fluorographies != null)
                {
                    return View(fluorographies);
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
